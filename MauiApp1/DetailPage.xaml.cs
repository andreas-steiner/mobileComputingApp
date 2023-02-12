using MauiApp1.Services;
using Shared;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly DataStore dataStore;

    public DetailPage(DataStore dataStore, Species Species)
	{
        InitializeComponent();

        SpeciesName.Text = Species.Name;
        SpeciesSpeed.Text = Species.Speed.ToString();
        SpeciesAlignement.Text = Species.Alignement;
        SpeciesAge.Text = Species.Age.ToString();
        SpeciesSize.Text = Species.Size;
        SpeciesLangs.Text = Species.Langs.First().Name;
        SpeciesTraits.Text = Species.Traits.First().Name;
        SpeciesSubRaces.Text = Species.SubRaces.First().Name;
        this.dataStore = dataStore;
    }
}