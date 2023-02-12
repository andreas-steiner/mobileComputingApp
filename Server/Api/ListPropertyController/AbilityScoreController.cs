using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api.ListPropertyController;

[Route("api/Species/{id:int}/[controller]")]
[ApiController]
[Authorize]
public class AbilityScoreController : ControllerBase {
	private readonly DataContext _dataContext;

	public AbilityScoreController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<AbilityScore>> Get(int id) {
		var species = _dataContext.Specien
			.Include(i => i.AbilityScores)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var abilities = species.AbilityScores;

		if(abilities == null || !abilities.Any()) {
			return NoContent();
		}

		return Ok(abilities);
	}

	[HttpPost]
	public ActionResult<AbilityScore> Post(int id, [FromBody] AbilityScore abilityScore) {
		var species = _dataContext.Specien
			.AsTracking()
			.Include(f => f.AbilityScores)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		abilityScore.Id = 0;
		abilityScore.LastEdited = DateTime.Now;
		abilityScore.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		species.AbilityScores.Add(abilityScore);

		_dataContext.Update(species);
		_dataContext.SaveChanges();

		return Ok(abilityScore);
	}

	[HttpPut]
	public ActionResult<AbilityScore> Put(int id, [FromBody] AbilityScore abilityScore) {
		var species = _dataContext.Specien
			.Include(i => i.AbilityScores)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbAbility = species.AbilityScores.FirstOrDefault(f => f.Id == abilityScore.Id);

		if(dbAbility is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbAbility.LastEdited != abilityScore.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbAbility);

		dbAbility.Name = abilityScore.Name;
		dbAbility.LastEdited = DateTime.Now;
		dbAbility.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbAbility);
		_dataContext.SaveChanges();

		return Ok(dbAbility);
	}

	[HttpDelete]
	public ActionResult Delete(int id, [FromBody] AbilityScore abilityScore) {
		var species = _dataContext.Specien
			.Include(i => i.AbilityScores)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbAbility = species.AbilityScores.FirstOrDefault(f => f.Id == abilityScore.Id);

		if(dbAbility is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbAbility.LastEdited != abilityScore.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbAbility);

		_dataContext.Remove(dbAbility);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
