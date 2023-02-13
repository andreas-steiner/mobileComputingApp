using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    public Species Species { get; private set; }
    public bool AsNew { get; private set; }
    public ObservableCollection<Lang> LangListe { get; set; }
    public ObservableCollection<Trait> TraitListe { get; set; }
    public ObservableCollection<SubRace> SubListe { get; set; }

    public DetailPage(Species species, bool asNew)
    {
        InitializeComponent();
        AsNew = asNew;

        Title = asNew ? "New Species" : "Edit Species";
		
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

        Species = species;
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

    public static DetailPage CreateNew() => new DetailPage(new Species(), true);

	public static DetailPage Edit(Species species) => new DetailPage(species, false);

    private async void OnSaveSpeciesClicked(object sender, EventArgs e)
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

        await DisplayAlert("Success", "Species saved", "Done");
        await Navigation.PopToRootAsync();
        OnDone.Invoke(this, Species);
        
        
    }

    public event EventHandler<Species> OnDone;
}