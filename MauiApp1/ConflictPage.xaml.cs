using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ConflictPage : ContentPage
{
    private readonly Species species = new Species();
    public ObservableCollection<Lang> LangListe { get; set; }
    public ObservableCollection<Trait> TraitListe { get; set; }
    public ObservableCollection<SubRace> SubListe { get; set; }
    public ObservableCollection<Lang> LangListeServer { get; set; }
    public ObservableCollection<Trait> TraitListeServer { get; set; }
    public ObservableCollection<SubRace> SubListeServer { get; set; }

    public ConflictPage(Species species, Species server)
    {
        InitializeComponent();

        this.species.Id = species.Id;
        this.species.LastEdited = species.LastEdited;
        this.species.LastEditFrom = server.LastEditFrom;

		SpeciesName.Text = species.Name;
        SpeciesSpeed.Text = species.Speed.ToString();
        SpeciesAlignement.Text = species.Alignement;
        SpeciesAge.Text = species.Age.ToString();
        SpeciesSize.Text = species.Size;

        SpeciesNameServer.Text = server.Name;
        SpeciesSpeedServer.Text = server.Speed.ToString();
        SpeciesAlignementServer.Text = server.Alignement;
        SpeciesAgeServer.Text = server.Age.ToString();
        SpeciesSizeServer.Text = server.Size;

        LangListe = new ObservableCollection<Lang>(species.Langs);
        LangList.ItemsSource = LangListe;
        TraitListe = new ObservableCollection<Trait>(species.Traits);
        TraitList.ItemsSource = TraitListe;
        SubListe = new ObservableCollection<SubRace>(species.SubRaces);
        SubList.ItemsSource = SubListe;

        LangListeServer = new ObservableCollection<Lang>(server.Langs);
        LangListServer.ItemsSource = LangListeServer;
        TraitListeServer = new ObservableCollection<Trait>(server.Traits);
        TraitListServer.ItemsSource = TraitListeServer;
        SubListeServer = new ObservableCollection<SubRace>(server.SubRaces);
        SubListServer.ItemsSource = SubListeServer;

		UserName.Text = "User: " + server.LastEditFrom;
		EditedTime.Text = server.LastEdited.ToString();
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
        species.Name = SpeciesName.Text;
        species.Speed = Int32.Parse(SpeciesSpeed.Text);
        species.Alignement = SpeciesAlignement.Text;
        species.Age = Int32.Parse(SpeciesAge.Text);
        species.Size = SpeciesSize.Text;
        species.Langs.Clear();
        species.Langs.AddRange(LangListe);
        species.Traits.Clear();
        species.Traits.AddRange(TraitListe);
        species.SubRaces.Clear();
        species.SubRaces.AddRange(SubListe);

        await DisplayAlert("Success", "Species saved with phone state", "Done");
        OnDone.Invoke(this, species);
        await Navigation.PopToRootAsync();
    }

    private async void OnSaveServerClicked(object sender, EventArgs e)
    {
        species.Name = SpeciesNameServer.Text;
        species.Speed = Int32.Parse(SpeciesSpeedServer.Text);
        species.Alignement = SpeciesAlignementServer.Text;
        species.Age = Int32.Parse(SpeciesAgeServer.Text);
        species.Size = SpeciesSizeServer.Text;
        species.Langs.Clear();
        species.Langs.AddRange(LangListeServer);
        species.Traits.Clear();
        species.Traits.AddRange(TraitListeServer);
        species.SubRaces.Clear();
        species.SubRaces.AddRange(SubListeServer);

        await DisplayAlert("Success", "Species saved with server state", "Done");
        OnDone.Invoke(this, species);
        await Navigation.PopToRootAsync();
    }

    public event EventHandler<Species> OnDone;

    private void OnRemoveLangClicked(object sender, EventArgs e)
    {
        var lang = (Lang)((Element)sender).Parent.Parent.BindingContext;
        LangListe.Remove(lang);
    }
    private void OnRemoveTraitClicked(object sender, EventArgs e)
    {
        var trait = (Trait)((Element)sender).Parent.Parent.BindingContext;
        TraitListe.Remove(trait);
    }
    private void OnRemoveSubClicked(object sender, EventArgs e)
    {
        var sub = (SubRace)((Element)sender).Parent.Parent.BindingContext;
        SubListe.Remove(sub);
    }
    private void OnRemoveLangServerClicked(object sender, EventArgs e)
    {
        var langServer = (Lang)((Element)sender).Parent.Parent.BindingContext;
        LangListeServer.Remove(langServer);
    }
    private void OnRemoveTraitServerClicked(object sender, EventArgs e)
    {
        var traitServer = (Trait)((Element)sender).Parent.Parent.BindingContext;
        TraitListeServer.Remove(traitServer);
    }
    private void OnRemoveSubServerClicked(object sender, EventArgs e)
    {
        var subServer = (SubRace)((Element)sender).Parent.Parent.BindingContext;
        SubListeServer.Remove(subServer);
    }
}