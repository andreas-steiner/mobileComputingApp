using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    public Species Species { get; private set; }
    public bool AsNew { get; private set; }

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

        LangList.ItemsSource = new ObservableCollection<Lang>(species.Langs);
        TraitList.ItemsSource = new ObservableCollection<Trait>(species.Traits);
        SubList.ItemsSource = new ObservableCollection<SubRace>(species.SubRaces);

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

    public static DetailPage CreateNew() => new DetailPage(null, true);

	public static DetailPage Edit(Species species) => new DetailPage(species, false);

    private async void OnSaveSpeciesClicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new ConflictPage(new Species(), true));
	}

    public event EventHandler<Species> OnDone;
}
