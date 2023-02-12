using Shared;

namespace MauiApp1.Services
{
    public interface DataStore
    {
        Task<List<Species>> AllSpecies();

		#region Species
		Task<Species> SpeciesGet(int id);

		Task<Species> SpeciesAdd(Species Species);

		/// <exception cref="DataStoreConflictDeletedException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task<Species> SpeciesUpdate(Species species, bool force = false);

		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task SpeciesRemove(Species species, bool force = false);
		#endregion

		#region Lang
		/// <exception cref="DataStoreCancleException"></exception>
		Task<Lang> LangGet(int speciesId, int id);

		/// <exception cref="DataStoreCancleException"></exception>
		Task<Lang> LangAdd(int id, Lang lang);

		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictDeletedException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task<Lang> LangUpdate(int id, Lang lang, bool force = false);

		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task LangRemove(int id, Lang lang, bool force = false);
		#endregion

		#region Trait
		/// <exception cref="DataStoreCancleException"></exception>
		Task<Trait> TraitGet(int speciesId, int id);

		/// <exception cref="DataStoreCancleException"></exception>
		Task<Trait> TraitAdd(int id, Trait trait);

		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictDeletedException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task<Trait> TraitUpdate(int id, Trait trait, bool force = false);


		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task TraitRemove(int id, Trait trait, bool force = false);
		#endregion

		#region SubRace
		/// <exception cref="DataStoreCancleException"></exception>
		Task<SubRace> SubRaceGet(int speciesId, int id);

		/// <exception cref="DataStoreCancleException"></exception>
		Task<SubRace> SubRaceAdd(int id, SubRace subRace);

		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictDeletedException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task<SubRace> SubRaceUpdate(int id, SubRace subRace, bool force = false);

		/// <exception cref="DataStoreCancleException"></exception>
		/// <exception cref="DataStoreConflictChangedException{T}"></exception>
		Task SubRaceRemove(int id, SubRace subRace, bool force = false);
		#endregion
	}
}
