using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ConflictPage : ContentPage
{
    private readonly DataStore dataStore;
    private readonly Species species;

    public ConflictPage(DataStore dataStore, Species species, bool alsNeu)
    {
        InitializeComponent();
        this.Title = alsNeu ? "New Species" : "Edit Species";
        SpeciesName.Text = species.Name;
        SpeciesSpeed.Text = species.Speed.ToString();
        SpeciesAlignement.Text = species.Alignement;
        SpeciesAge.Text = species.Age.ToString();
        SpeciesSize.Text = species.Size;
        LangList.ItemsSource = new ObservableCollection<Lang>(species.Langs);
        LangListServer.ItemsSource = new ObservableCollection<Lang>(species.Langs);
        TraitList.ItemsSource = new ObservableCollection<Trait>(species.Traits);
        TraitListServer.ItemsSource = new ObservableCollection<Trait>(species.Traits);
        SubList.ItemsSource = new ObservableCollection<SubRace>(species.SubRaces);
        SubListServer.ItemsSource = new ObservableCollection<SubRace>(species.SubRaces);


        this.dataStore = dataStore;
        this.species = species;
    }

    public void OnAddLangClicked(object sender, EventArgs args)
    {
        ((ObservableCollection<Lang>)LangList.ItemsSource).Add(new Lang() { Name = "new Lang" });
    }
    public void OnAddTraitClicked(object sender, EventArgs args)
    {
        ((ObservableCollection<Trait>)TraitList.ItemsSource).Add(new Trait() { Name = "new Trait" });
    }
    public void OnAddSubClicked(object sender, EventArgs args)
    {
        ((ObservableCollection<SubRace>)SubList.ItemsSource).Add(new SubRace() { Name = "new Sub Race" });
    }

    public void OnAddLangClickedServer(object sender, EventArgs args)
    {
        ((ObservableCollection<Lang>)LangListServer.ItemsSource).Add(new Lang() { Name = "new Lang" });
    }
    public void OnAddTraitClickedServer(object sender, EventArgs args)
    {
        ((ObservableCollection<Trait>)TraitListServer.ItemsSource).Add(new Trait() { Name = "new Trait" });
    }
    public void OnAddSubClickedServer(object sender, EventArgs args)
    {
        ((ObservableCollection<SubRace>)SubListServer.ItemsSource).Add(new SubRace() { Name = "new Sub Race" });
    }
}