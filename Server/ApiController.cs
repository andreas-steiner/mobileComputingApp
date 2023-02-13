using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server;

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
		species.Langs.ForEach(f => f.Id = 0);
		species.Traits.ForEach(f => f.Id = 0);
		species.SubRaces.ForEach(f => f.Id = 0);

		_dataContext.Add(species);
		_dataContext.SaveChanges();

		return Ok(species);
	}

	[HttpPut]
	public ActionResult<Trait> Put([FromBody] Species species) {
		var dbSpecies = _dataContext.SpecienIncludingAll.FirstOrDefault(f => f.Id == species.Id);

		if (dbSpecies is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if (dbSpecies.LastEdited != species.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbSpecies);

		var modifyDatetime = DateTime.Now;
		var user = User.Claims.First(f => f.Type == "nickname").Value;

		dbSpecies.Name = species.Name;
		dbSpecies.Size = species.Size;
		dbSpecies.Age = species.Age;
		dbSpecies.Alignement = species.Alignement;
		dbSpecies.Speed = species.Speed;
		dbSpecies.LastEdited = DateTime.Now;
		dbSpecies.LastEditFrom = user;

		dbSpecies.Langs.RemoveAll(r => !species.Langs.Any(a => a.Id == r.Id));
		foreach (var lang in dbSpecies.Langs) {
			var newLang = species.Langs.First(f => f.Id == lang.Id);

			if (lang.Name == newLang.Name)
				continue;

			lang.Name = newLang.Name;
		}
		var newLangs = species.Langs.Where(w => !dbSpecies.Langs.Any(a => a.Id == w.Id)).ToList();
		newLangs.ForEach(f => f.Id = 0);
		dbSpecies.Langs.AddRange(newLangs);


		dbSpecies.Traits.RemoveAll(r => !species.Traits.Any(a => a.Id == r.Id));
		foreach (var trait in dbSpecies.Traits) {
			var newTrait = species.Traits.First(f => f.Id == trait.Id);

			if (trait.Name == newTrait.Name)
				continue;

			trait.Name = newTrait.Name;
		}
		var newTraits = species.Traits.Where(w => !dbSpecies.Traits.Any(a => a.Id == w.Id)).ToList();
		newTraits.ForEach(f => f.Id = 0);
		dbSpecies.Traits.AddRange(newTraits);


		dbSpecies.SubRaces.RemoveAll(r => !species.SubRaces.Any(a => a.Id == r.Id));
		foreach (var subRace in dbSpecies.SubRaces) {
			var newSubRace = species.SubRaces.First(f => f.Id == subRace.Id);

			if (subRace.Name == newSubRace.Name)
				continue;

			subRace.Name = newSubRace.Name;
		}
		var newSubRaces = species.SubRaces.Where(w => !dbSpecies.SubRaces.Any(a => a.Id == w.Id)).ToList();
		newSubRaces.ForEach(f => f.Id = 0);
		dbSpecies.SubRaces.AddRange(newSubRaces);


		_dataContext.Update(dbSpecies);
		_dataContext.SaveChanges();

		return Ok(dbSpecies);
	}

	[HttpDelete]
	public ActionResult Delete([FromBody] Species species) {
		var dbSpecies = _dataContext.SpecienIncludingAll.FirstOrDefault(f => f.Id == species.Id);

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
