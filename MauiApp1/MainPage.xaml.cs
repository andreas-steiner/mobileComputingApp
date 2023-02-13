using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;
using Windows.UI.WebUI;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly DataStore _dataStore;
    private ObservableCollection<Species> _species;


	public MainPage(DataStore dataStore)
    {
        InitializeComponent();

		Task.Run(async () => {
			var data = await dataStore.AllSpecies();
            _species = new ObservableCollection<Species>(data);
			list.ItemsSource = _species;
		});

        _dataStore = dataStore;
    }

    async void OnSpeciesTapped(object sender, ItemTappedEventArgs e)
    {
        var species = new Species().CopyFrom((Species)list.SelectedItem);
		var detailPage = DetailPage.Edit(species);

		await Navigation.PushAsync(detailPage);

		if (detailPage.Species is null)
			return;

		try {
			var newSpecies = await _dataStore.SpeciesUpdate(detailPage.Species);

			((Species)list.SelectedItem).CopyFrom(newSpecies);

		} catch (DataStoreConflictDeletedException deleted) {
			// TODO conflict beheben
		} catch (DataStoreConflictChangedException<Species> changed) {
			// TODO conflict beheben
		}
	}

    public async void OnAddClicked(object sender, EventArgs args)
	{
		var detailPage = DetailPage.CreateNew();
		
		await Navigation.PushAsync(detailPage);

		if (detailPage.Species is null)
			return;

		var newSpecies = await _dataStore.SpeciesAdd(detailPage.Species);
		_species.Add(newSpecies);
	}

	private async void Button_Clicked(object sender, EventArgs e) {
        var species = (Species)((Button)sender).Parent.Parent.BindingContext;
        try {
			await _dataStore.SpeciesRemove(species);
			_species.Remove(species);
		} catch (DataStoreConflictChangedException<Species> conflict) {
			// TODO conflict beheben
			
		}
	}
}
