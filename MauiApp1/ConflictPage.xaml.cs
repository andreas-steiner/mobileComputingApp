using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ConflictPage : ContentPage
{
    public Species Species { get; private set; }
    private readonly Species species;
    public ObservableCollection<Lang> LangListe { get; set; }
    public ObservableCollection<Trait> TraitListe { get; set; }
    public ObservableCollection<SubRace> SubListe { get; set; }
    public ObservableCollection<Lang> LangListeServer { get; set; }
    public ObservableCollection<Trait> TraitListeServer { get; set; }
    public ObservableCollection<SubRace> SubListeServer { get; set; }

    public ConflictPage(Species species, Species server)
    {
        InitializeComponent();

        SpeciesName.Text = species.Name;
        SpeciesSpeed.Text = species.Speed.ToString();
        SpeciesAlignement.Text = species.Alignement;
        SpeciesAge.Text = species.Age.ToString();
        SpeciesSize.Text = species.Size;

        LangListe = new ObservableCollection<Lang>(species.Langs);
        LangList.ItemsSource = LangListe;
        TraitListe = new ObservableCollection<Trait>(species.Traits);
        TraitList.ItemsSource = TraitListe;
        SubListe = new ObservableCollection<SubRace>(species.SubRaces);
        SubList.ItemsSource = SubListe;

        LangListeServer = new ObservableCollection<Lang>(species.Langs);
        LangListServer.ItemsSource = LangListeServer;
        TraitListeServer = new ObservableCollection<Trait>(species.Traits);
        TraitListServer.ItemsSource = TraitListeServer;
        SubListeServer = new ObservableCollection<SubRace>(species.SubRaces);
        SubListServer.ItemsSource = SubListeServer;

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

    public async void OnCancelClicked(object sender, EventArgs args)
    {
        await Navigation.PopToRootAsync();
    }

    private async void OnSavePhoneClicked(object sender, EventArgs e)
    {
        Species.Name = SpeciesName.Text;
        Species.Speed = Int32.Parse(SpeciesSpeed.Text);
        Species.Alignement = SpeciesAlignement.Text;
        Species.Age = Int32.Parse(SpeciesAge.Text);
        Species.Size = SpeciesSize.Text;
        Species.Langs.Clear();
        Species.Langs.AddRange(LangListe);
        Species.Traits.Clear();
        Species.Traits.AddRange(TraitListe);
        Species.SubRaces.Clear();
        Species.SubRaces.AddRange(SubListe);

        await DisplayAlert("Success", "Species saved with phone state", "Done");
        OnDone.Invoke(this, Species);
        await Navigation.PopToRootAsync();
    }

    private async void OnSaveServerClicked(object sender, EventArgs e)
    {
        Species.Name = SpeciesNameServer.Text;
        Species.Speed = Int32.Parse(SpeciesSpeedServer.Text);
        Species.Alignement = SpeciesAlignementServer.Text;
        Species.Age = Int32.Parse(SpeciesAgeServer.Text);
        Species.Size = SpeciesSizeServer.Text;
        Species.Langs.Clear();
        Species.Langs.AddRange(LangListeServer);
        Species.Traits.Clear();
        Species.Traits.AddRange(TraitListeServer);
        Species.SubRaces.Clear();
        Species.SubRaces.AddRange(SubListeServer);

        await DisplayAlert("Success", "Species saved with server state", "Done");
        OnDone.Invoke(this, Species);
        await Navigation.PopToRootAsync();
    }

    public event EventHandler<Species> OnDone;
}