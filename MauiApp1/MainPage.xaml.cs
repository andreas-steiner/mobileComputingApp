using CommunityToolkit.Maui.Views;
using MauiApp1.Services;
using Shared;
using System;
using System.Collections.ObjectModel;
using System.Threading.Channels;

namespace MauiApp1;

public partial class MainPage : ContentPage {
	private readonly DataStore _dataStore;
	private ObservableCollection<Species> _species;


	public MainPage(DataStore dataStore) {
		InitializeComponent();

		Task.Run(async () => {
			var data = await dataStore.AllSpecies();
			_species = new ObservableCollection<Species>(data);
			list.ItemsSource = _species;
		});

		_dataStore = dataStore;
	}

	async void OnSpeciesTapped(object sender, ItemTappedEventArgs e) {
		var species = new Species().CopyFrom((Species)list.SelectedItem);
		var detailPage = DetailPage.Edit(species);

		detailPage.OnDone += DetailPage_OnDone;

		await Navigation.PushAsync(detailPage);
	}

	public async void OnAddClicked(object sender, EventArgs args) {
		var detailPage = DetailPage.CreateNew();

		detailPage.OnDone += DetailPage_OnDone;

		await Navigation.PushAsync(detailPage);
	}

	private async void DetailPage_OnDone(object sender, Species species) {
		var detailPage = (DetailPage)sender;
		if (detailPage.AsNew) {
			await SpeciesNew(species);
		} else {
			await SpeciesUpdate(species);
		}
	}

	private async Task SpeciesUpdate(Species species) {
		try {
			var newSpecies = await _dataStore.SpeciesUpdate(species);
			((Species)list.SelectedItem).CopyFrom(newSpecies);
		} catch (DataStoreConflictDeletedException) {
			var popUp = new MsgPopUp($"Species {species.Name} has been deleted, do you want to recreate it as new?") {
				CanBeDismissedByTappingOutsideOfPopup = false
			};

			var shouldRecreate = (bool)await this.ShowPopupAsync(popUp);

			if (shouldRecreate) {
				await _dataStore.SpeciesAdd(species);
			} else {
				_species.Remove((Species)list.SelectedItem);
			}
		} catch (DataStoreConflictChangedException<Species> changed) {
			var conflictPage = new ConflictPage(species, changed.ConflicObject);

			((Species)list.SelectedItem).CopyFrom(changed.ConflicObject);
			conflictPage.OnDone += ConflictPage_OneDone;

			await Navigation.PushAsync(conflictPage);
		}
	}

	private async void ConflictPage_OneDone(object sender, Species species) {
		var newSpecies = await _dataStore.SpeciesUpdate(species, true);
		((Species)list.SelectedItem).CopyFrom(newSpecies);
	}

	private async Task SpeciesNew(Species species) {
		var newSpecies = await _dataStore.SpeciesAdd(species);
		_species.Add(newSpecies);
	}

	private async void BtnDelete_Clicked(object sender, EventArgs e) {
		var species = (Species)((Button)sender).Parent.Parent.BindingContext;
		try {
			await _dataStore.SpeciesRemove(species);
			_species.Remove(species);
		} catch (DataStoreConflictChangedException<Species> deleted) {
			var popUp = new MsgPopUp($"Species {species.Name} has change, do you still want to delete?") {
				CanBeDismissedByTappingOutsideOfPopup = false
			};

			var shouldDelete = (bool)await this.ShowPopupAsync(popUp);

			if (shouldDelete) {
				await _dataStore.SpeciesRemove(species, true);
			} else {
				((Species)list.SelectedItem).CopyFrom(deleted.ConflicObject);
			}
		}
	}
}
