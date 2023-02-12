using Shared;

namespace MauiApp1.Services
{
    public interface DataStore
    {
        Task<List<Species>> AllSpecies();

		Task<Species> SpeciesAdd(Species Species);
        Task<Species> SpeciesUpdate(Species species);
		Task SpeciesRemove(Species species);

		Task<Lang> LangAdd(int id, Lang lang);
		Task<Lang> LangUpdate(int id, Lang lang);
		Task LangRemove(int id, Lang lang);

		Task<Trait> TraitAdd(int id, Trait trait);
		Task<Trait> TraitUpdate(int id, Trait trait);
		Task TraitRemove(int id, Trait trait);

		Task<SubRace> SubRaceAdd(int id, SubRace subRace);
		Task<SubRace> SubRaceUpdate(int id, SubRace subRace);
		Task SubRaceRemove(int id, SubRace subRace);
	}
}
