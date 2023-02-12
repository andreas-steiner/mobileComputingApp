using Shared;

namespace MauiApp1.Services {
	public class MockDataStore : DataStore {
		private List<Species> _species;
		private Random rnd = new Random();

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

			dbSpecies.Name = species.Name;
			dbSpecies.Size = species.Size;
			dbSpecies.Age = species.Age;
			dbSpecies.Alignement = species.Alignement;
			dbSpecies.Speed = species.Speed;
			dbSpecies.LastEdited = DateTime.Now;
			dbSpecies.LastEditFrom = "local";

			return Task.FromResult(dbSpecies);
		}
		public Task SpeciesRemove(Species species, bool force = false) {
			var dbSpecies = _species.FirstOrDefault(f => f.Id == species.Id);

			if (dbSpecies is null)
				return Task.CompletedTask;

			_species.Remove(dbSpecies);

			return Task.CompletedTask;
		}


		public Task<Lang> LangGet(int speciesId, int id) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{speciesId}'.");

			return Task.FromResult(species.Langs.FirstOrDefault(f => f.Id == id));
		}
		public Task<Lang> LangAdd(int id, Lang lang) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			lang.Id = species.Langs.Max(m => m.Id) + 1;
			lang.LastEdited = DateTime.Now;
			lang.LastEditFrom = "local";

			species.Langs.Add(lang);

			return Task.FromResult(lang);
		}
		public Task<Lang> LangUpdate(int id, Lang lang, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbLang = species.Langs.FirstOrDefault(f => f.Id == lang.Id);

			if (dbLang is null)
				throw new DataStoreConflictDeletedException();

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<Lang>(new Lang() {
					Id = dbLang.Id,
					LastEdited = dbLang.LastEdited.AddSeconds(1),
					Name = dbLang.Name + " neuer Wert",
					LastEditFrom = dbLang.LastEditFrom + "2",
				});

			dbLang.Name = lang.Name;
			dbLang.LastEdited = DateTime.Now;
			dbLang.LastEditFrom = "local";

			return Task.FromResult(dbLang);
		}
		public Task LangRemove(int id, Lang lang, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbLang = species.Langs.FirstOrDefault(f => f.Id == lang.Id);

			if (dbLang is null)
				return Task.CompletedTask;

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<Lang>(new Lang() {
					Id = dbLang.Id,
					LastEdited = dbLang.LastEdited.AddSeconds(1),
					Name = dbLang.Name + " neuer Wert",
					LastEditFrom = dbLang.LastEditFrom + "2",
				});

			species.Langs.Remove(dbLang);

			return Task.CompletedTask;
		}


		public Task<Trait> TraitGet(int speciesId, int id) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{speciesId}'.");

			return Task.FromResult(species.Traits.FirstOrDefault(f => f.Id == id));
		}
		public Task<Trait> TraitAdd(int id, Trait trait) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			trait.Id = species.Langs.Max(m => m.Id) + 1;
			trait.LastEdited = DateTime.Now;
			trait.LastEditFrom = "local";

			species.Traits.Add(trait);

			return Task.FromResult(trait);
		}
		public Task<Trait> TraitUpdate(int id, Trait trait, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbTrait = species.Traits.FirstOrDefault(f => f.Id == trait.Id);

			if (dbTrait is null)
				throw new DataStoreConflictDeletedException();

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<Trait>(new Trait() {
					Id = dbTrait.Id,
					LastEdited = dbTrait.LastEdited.AddSeconds(1),
					Name = dbTrait.Name + " neuer Wert",
					LastEditFrom = dbTrait.LastEditFrom + "2",
				});

			dbTrait.Name = trait.Name;
			dbTrait.LastEdited = DateTime.Now;
			dbTrait.LastEditFrom = "local";

			return Task.FromResult(dbTrait);
		}
		public Task TraitRemove(int id, Trait trait, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbTrait = species.Traits.FirstOrDefault(f => f.Id == trait.Id);

			if (dbTrait is null)
				return Task.CompletedTask;

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<Trait>(new Trait() {
					Id = dbTrait.Id,
					LastEdited = dbTrait.LastEdited.AddSeconds(1),
					Name = dbTrait.Name + " neuer Wert",
					LastEditFrom = dbTrait.LastEditFrom + "2",
				});

			species.Traits.Remove(dbTrait);

			return Task.CompletedTask;
		}


		public Task<SubRace> SubRaceGet(int speciesId, int id) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{speciesId}'.");

			return Task.FromResult(species.SubRaces.FirstOrDefault(f => f.Id == id));
		}
		public Task<SubRace> SubRaceAdd(int id, SubRace subRace) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			subRace.Id = species.Langs.Max(m => m.Id) + 1;
			subRace.LastEdited = DateTime.Now;
			subRace.LastEditFrom = "local";

			species.SubRaces.Add(subRace);

			return Task.FromResult(subRace);
		}
		public Task<SubRace> SubRaceUpdate(int id, SubRace subRace, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbSubRace = species.SubRaces.FirstOrDefault(f => f.Id == subRace.Id);

			if (dbSubRace is null)
				throw new DataStoreConflictDeletedException();

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<SubRace>(new SubRace() {
					Id = dbSubRace.Id,
					LastEdited = dbSubRace.LastEdited.AddSeconds(1),
					Name = dbSubRace.Name + " neuer Wert",
					LastEditFrom = dbSubRace.LastEditFrom + "2",
				});

			dbSubRace.Name = subRace.Name;
			dbSubRace.LastEdited = DateTime.Now;
			dbSubRace.LastEditFrom = "local";

			return Task.FromResult(dbSubRace);
		}
		public Task SubRaceRemove(int id, SubRace subRace, bool force = false) {
			var species = _species
				.FirstOrDefault(f => f.Id == id);

			if (species == null)
				throw new DataStoreCancleException($"Couldn't find species with id '{id}'.");

			var dbSubRace = species.SubRaces.FirstOrDefault(f => f.Id == subRace.Id);

			if (dbSubRace is null)
				return Task.CompletedTask;

			if (rnd.Next(2) == 1 || !force)
				throw new DataStoreConflictChanedException<SubRace>(new SubRace() {
					Id = dbSubRace.Id,
					LastEdited = dbSubRace.LastEdited.AddSeconds(1),
					Name = dbSubRace.Name + " neuer Wert",
					LastEditFrom = dbSubRace.LastEditFrom + "2",
				});

			species.SubRaces.Remove(dbSubRace);

			return Task.CompletedTask;
		}
	}
}
