using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ConflictPage : ContentPage
{
    public Species Species { get; private set; }
    private readonly Species species;

    public ConflictPage(Species species, Species server)
    {
        InitializeComponent();

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

	public event EventHandler<Species> OnDone;
}