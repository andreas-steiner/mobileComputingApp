using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api.ListPropertyController;

[Route("api/Species/{id:int}/[controller]")]
[ApiController]
[Authorize]
public class StartingProficencyController : ControllerBase {
	private readonly DataContext _dataContext;

	public StartingProficencyController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<StartingProficency>> Get(int id) {
		var species = _dataContext.Specien
			.Include(i => i.StartingProficencies)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var abilities = species.StartingProficencies;

		if(abilities == null || !abilities.Any()) {
			return NoContent();
		}

		return Ok(abilities);
	}

	[HttpPost]
	public ActionResult<StartingProficency> Post(int id, [FromBody] StartingProficency startingProficency) {
		var species = _dataContext.Specien
			.AsTracking()
			.Include(f => f.StartingProficencies)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		startingProficency.Id = 0;
		startingProficency.LastEdited = DateTime.Now;
		startingProficency.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		species.StartingProficencies.Add(startingProficency);

		_dataContext.Update(species);
		_dataContext.SaveChanges();

		return Ok(startingProficency);
	}

	[HttpPut]
	public ActionResult<StartingProficency> Put(int id, [FromBody] StartingProficency startingProficency) {
		var species = _dataContext.Specien
			.Include(i => i.StartingProficencies)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbProficency = species.StartingProficencies.FirstOrDefault(f => f.Id == startingProficency.Id);

		if(dbProficency is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbProficency.LastEdited != startingProficency.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbProficency);

		dbProficency.Name = startingProficency.Name;
		dbProficency.LastEdited = DateTime.Now;
		dbProficency.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbProficency);
		_dataContext.SaveChanges();

		return Ok(dbProficency);
	}

	[HttpDelete]
	public ActionResult Delete(int id, [FromBody] StartingProficency startingProficency) {
		var species = _dataContext.Specien
			.Include(i => i.StartingProficencies)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbProficency = species.StartingProficencies.FirstOrDefault(f => f.Id == startingProficency.Id);

		if(dbProficency is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbProficency.LastEdited != startingProficency.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbProficency);

		_dataContext.Remove(dbProficency);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
