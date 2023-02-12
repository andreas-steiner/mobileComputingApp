using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api.ListPropertyController;

[Route("api/Species/{id:int}/[controller]")]
[ApiController]
[Authorize]
public class TraitController : ControllerBase {
	private readonly DataContext _dataContext;

	public TraitController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Trait>> Get(int id) {
		var species = _dataContext.Specien
			.Include(i => i.Traits)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var abilities = species.Traits;

		if(abilities == null || !abilities.Any()) {
			return NoContent();
		}

		return Ok(abilities);
	}

	[HttpPost]
	public ActionResult<Trait> Post(int id, [FromBody] Trait trait) {
		var species = _dataContext.Specien
			.AsTracking()
			.Include(f => f.Traits)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		trait.Id = 0;
		trait.LastEdited = DateTime.Now;
		trait.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		species.Traits.Add(trait);

		_dataContext.Update(species);
		_dataContext.SaveChanges();

		return Ok(trait);
	}

	[HttpPut]
	public ActionResult<Trait> Put(int id, [FromBody] Trait trait) {
		var species = _dataContext.Specien
			.Include(i => i.Traits)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbTrait = species.Traits.FirstOrDefault(f => f.Id == trait.Id);

		if(dbTrait is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbTrait.LastEdited != trait.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbTrait);

		dbTrait.Name = trait.Name;
		dbTrait.LastEdited = DateTime.Now;
		dbTrait.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbTrait);
		_dataContext.SaveChanges();

		return Ok(dbTrait);
	}

	[HttpDelete]
	public ActionResult Delete(int id, [FromBody] Trait trait) {
		var species = _dataContext.Specien
			.Include(i => i.Traits)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbTrait = species.Traits.FirstOrDefault(f => f.Id == trait.Id);

		if(dbTrait is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbTrait.LastEdited != trait.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbTrait);

		_dataContext.Remove(dbTrait);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
