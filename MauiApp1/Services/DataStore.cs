using Shared;

namespace MauiApp1.Services;

public interface DataStore
{
    Task<List<Species>> AllSpecies();

	Task<Species> SpeciesGet(int id);

	Task<Species> SpeciesAdd(Species Species);

	/// <exception cref="DataStoreConflictDeletedException"></exception>
	/// <exception cref="DataStoreConflictChangedException{T}"></exception>
	Task<Species> SpeciesUpdate(Species species, bool force = false);

	/// <exception cref="DataStoreConflictChangedException{T}"></exception>
	Task SpeciesRemove(Species species, bool force = false);
}
