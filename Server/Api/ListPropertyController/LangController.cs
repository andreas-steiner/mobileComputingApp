using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Api.ListPropertyController;

[Route("api/Species/{id:int}/[controller]")]
[ApiController]
[Authorize]
public class LangController : ControllerBase {
	private readonly DataContext _dataContext;

	public LangController(DataContext dataContext) {
		_dataContext = dataContext;
	}

	[HttpGet]
	public ActionResult<IEnumerable<Lang>> Get(int id) {
		var species = _dataContext.Specien
			.Include(i => i.Langs)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var abilities = species.Langs;

		if(abilities == null || !abilities.Any()) {
			return NoContent();
		}

		return Ok(abilities);
	}

	[HttpPost]
	public ActionResult<Lang> Post(int id, [FromBody] Lang Lang) {
		var species = _dataContext.Specien
			.AsTracking()
			.Include(f => f.Langs)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		Lang.Id = 0;
		Lang.LastEdited = DateTime.Now;
		Lang.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		species.Langs.Add(Lang);

		_dataContext.Update(species);
		_dataContext.SaveChanges();

		return Ok(Lang);
	}

	[HttpPut]
	public ActionResult<Lang> Put(int id, [FromBody] Lang Lang) {
		var species = _dataContext.Specien
			.Include(i => i.Langs)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbLang = species.Langs.FirstOrDefault(f => f.Id == Lang.Id);

		if(dbLang is null) {
			return NotFound();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbLang.LastEdited != Lang.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbLang);

		dbLang.Name = Lang.Name;
		dbLang.LastEdited = DateTime.Now;
		dbLang.LastEditFrom = User.Claims.First(f => f.Type == "nickname").Value;

		_dataContext.Update(dbLang);
		_dataContext.SaveChanges();

		return Ok(dbLang);
	}

	[HttpDelete]
	public ActionResult Delete(int id, [FromBody] Lang Lang) {
		var species = _dataContext.Specien
			.Include(i => i.Langs)
			.FirstOrDefault(f => f.Id == id);

		if(species == null) {
			return NotFound($"Couldn't find species with id '{id}'.");
		}

		var dbLang = species.Langs.FirstOrDefault(f => f.Id == Lang.Id);

		if(dbLang is null) {
			return NoContent();
		}

		var force = Request.Headers.Any(w => w.Key == "Force");

		if(dbLang.LastEdited != Lang.LastEdited && !force)
			return StatusCode(StatusCodes.Status409Conflict, dbLang);

		_dataContext.Remove(dbLang);
		_dataContext.SaveChanges();

		return NoContent();
	}
}
