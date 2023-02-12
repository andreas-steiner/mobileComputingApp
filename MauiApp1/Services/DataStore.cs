using Shared;

namespace MauiApp1.Services
{
    public interface DataStore
    {
        Task<List<Species>> AllSpecies();

		Task<Species> SpeciesAdd(Species Species);
        Task<Species> SpeciesUpdate(Species species, bool force = false);
		Task SpeciesRemove(Species species, bool force = false);

		Task<Lang> LangAdd(int id, Lang lang);
		Task<Lang> LangUpdate(int id, Lang lang, bool force = false);
		Task LangRemove(int id, Lang lang, bool force = false);

		Task<Trait> TraitAdd(int id, Trait trait);
		Task<Trait> TraitUpdate(int id, Trait trait, bool force = false);
		Task TraitRemove(int id, Trait trait, bool force = false);

		Task<SubRace> SubRaceAdd(int id, SubRace subRace);
		Task<SubRace> SubRaceUpdate(int id, SubRace subRace, bool force = false);
		Task SubRaceRemove(int id, SubRace subRace, bool force = false);
	}
}
