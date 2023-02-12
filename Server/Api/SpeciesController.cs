using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpeciesController : ControllerBase {
	private readonly DataContext _dataContext;

	public SpeciesController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Species>> Get() {


		return Ok(_dataContext.SpecienIncludingAll);
	}

	[HttpGet("{id:int}")]
	public ActionResult<Species> Get(int id) {
		var res = _dataContext.SpecienIncludingAll.FirstOrDefault(f => f.Id == id);

		if (res == null)
			return NotFound();

		return Ok(res);
	}

	[HttpPost]
	public ActionResult<Species> Post([FromBody] Species species) {
		species.Id = 0;
		species.LastEdited = DateTime.Now;
		species.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Add(species);
		_dataContext.SaveChanges();

		return Ok(species);
	}

	[HttpPut]
	public ActionResult<Trait> Put([FromBody] Species species) {
		var dbSpecies = _dataContext.Specien.FirstOrDefault(f => f.Id == species.Id);

		if (dbSpecies is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if (dbSpecies.LastEdited != species.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbSpecies);

		dbSpecies.Name = species.Name;
		dbSpecies.Size = species.Size;
		dbSpecies.Age = species.Age;
		dbSpecies.Alignement = species.Alignement;
		dbSpecies.Speed = species.Speed;
		dbSpecies.LastEdited = DateTime.Now;
		dbSpecies.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbSpecies);
		_dataContext.SaveChanges();

		return Ok(dbSpecies);
	}

	[HttpDelete]
	public ActionResult Delete([FromBody] Species species) {
		var dbSpecies = species.Traits.FirstOrDefault(f => f.Id == species.Id);

		if (dbSpecies is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if (dbSpecies.LastEdited != species.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbSpecies);

		_dataContext.Remove(dbSpecies);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
