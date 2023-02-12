using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api.ListPropertyController;

[Route("api/Species/{id:int}/[controller]")]
[ApiController]
[Authorize]
public class SubRaceController : ControllerBase {
	private readonly DataContext _dataContext;

	public SubRaceController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<SubRace>> Get(int id) {
		var species = _dataContext.Specien
			.Include(i => i.SubRaces)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var abilities = species.SubRaces;

		if(abilities == null || !abilities.Any()) {
			return NoContent();
		}

		return Ok(abilities);
	}

	[HttpPost]
	public ActionResult<SubRace> Post(int id, [FromBody] SubRace subRace) {
		var species = _dataContext.Specien
			.AsTracking()
			.Include(f => f.SubRaces)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		subRace.Id = 0;
		subRace.LastEdited = DateTime.Now;
		subRace.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		species.SubRaces.Add(subRace);

		_dataContext.Update(species);
		_dataContext.SaveChanges();

		return Ok(subRace);
	}

	[HttpPut]
	public ActionResult<SubRace> Put(int id, [FromBody] SubRace subRace) {
		var species = _dataContext.Specien
			.Include(i => i.SubRaces)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbSubRace = species.SubRaces.FirstOrDefault(f => f.Id == subRace.Id);

		if(dbSubRace is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbSubRace.LastEdited != subRace.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbSubRace);

		dbSubRace.Name = subRace.Name;
		dbSubRace.LastEdited = DateTime.Now;
		dbSubRace.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbSubRace);
		_dataContext.SaveChanges();

		return Ok(dbSubRace);
	}

	[HttpDelete]
	public ActionResult Delete(int id, [FromBody] SubRace subRace) {
		var species = _dataContext.Specien
			.Include(i => i.SubRaces)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbSubRace = species.SubRaces.FirstOrDefault(f => f.Id == subRace.Id);

		if(dbSubRace is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbSubRace.LastEdited != subRace.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbSubRace);

		_dataContext.Remove(dbSubRace);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
