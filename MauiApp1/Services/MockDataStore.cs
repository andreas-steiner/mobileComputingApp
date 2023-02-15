using Shared;

namespace MauiApp1.Services;

public class MockDataStore : DataStore {
	private List<Species> _species;
	private Random rnd = new Random();

	private readonly bool _enableRnd = false;

	public Task<List<Species>> AllSpecies() {
		if (_species is null)
			_species = MockData.GetData();

		return Task.FromResult(_species);
	}

	public Task<Species> SpeciesGet(int id) {
		return Task.FromResult(_species.FirstOrDefault(f => f.Id == id));
	}
	public Task<Species> SpeciesAdd(Species species) {
		species.Id = _species.Max(m => m.Id) + 1;
		species.LastEdited = DateTime.Now;
		species.LastEditFrom = "local";

		_species.Add(species);

		return Task.FromResult(species);
	}
	public Task<Species> SpeciesUpdate(Species species, bool force = false) {
		var dbSpecies = _species.FirstOrDefault(f => f.Id == species.Id);

		if (dbSpecies is null)
			throw new DataStoreConflictDeletedException();

		if (_enableRnd && rnd.Next(2) == 1 && !force) {
			species = new Species().CopyFrom(dbSpecies);
			species.LastEdited = dbSpecies.LastEdited.AddSeconds(1);
			species.Name = dbSpecies.Name + " neuer Wert";
			species.LastEditFrom = dbSpecies.LastEditFrom + "2";

			throw new DataStoreConflictChangedException(species);
		}

		dbSpecies.Name = species.Name;
		dbSpecies.Size = species.Size;
		dbSpecies.Age = species.Age;
		dbSpecies.Alignement = species.Alignement;
		dbSpecies.Speed = species.Speed;
		dbSpecies.Langs = species.Langs;
		dbSpecies.Traits = species.Traits;
		dbSpecies.SubRaces = species.SubRaces;
		dbSpecies.LastEdited = DateTime.Now;
		dbSpecies.LastEditFrom = "local";

		return Task.FromResult(dbSpecies);
	}
	public Task SpeciesRemove(Species species, bool force = false) {
		var dbSpecies = _species.FirstOrDefault(f => f.Id == species.Id);

		if (dbSpecies is null)
			return Task.CompletedTask;

		if (_enableRnd && rnd.Next(2) == 1 && !force) {
			species = new Species().CopyFrom(dbSpecies);
			species.LastEdited = dbSpecies.LastEdited.AddSeconds(1);
			species.Name = dbSpecies.Name + " neuer Wert";
			species.LastEditFrom = dbSpecies.LastEditFrom + "2";

			throw new DataStoreConflictChangedException(species);
		}

		_species.Remove(dbSpecies);

		return Task.CompletedTask;
	}

	public void SetIdToken(string token) { }
}
