using CommunityToolkit.Maui.Views;
using MauiApp1.Services;
using Shared;
using System;
using System.Collections.ObjectModel;
using System.Threading.Channels;

namespace MauiApp1;

public partial class MainPage : ContentPage {
	public DataStore DataStore { get; private set; }
	private ObservableCollection<Species> _species;


	public MainPage(DataStore dataStore) {
		InitializeComponent();
		
        DataStore = dataStore;
    }

	protected async override void OnAppearing() {
		base.OnAppearing();
		await Refresh();
	}

	public async Task Refresh()
	{
        var data = await DataStore.AllSpecies();
        _species = new ObservableCollection<Species>(data);
		//await DisplayAlert("Info", $"Daten geladen: {_species.Count}", "Okay");
        list.ItemsSource = _species;
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
			var newSpecies = await DataStore.SpeciesUpdate(species);
			((Species)list.SelectedItem).CopyFrom(newSpecies);
			await Refresh();
        } catch (DataStoreConflictDeletedException) {
			var popUp = new MsgPopUp($"Species {species.Name} has been deleted, do you want to recreate it as new?") {
				CanBeDismissedByTappingOutsideOfPopup = false
			};

			var shouldRecreate = (bool)await this.ShowPopupAsync(popUp);

			if (shouldRecreate) {
				await DataStore.SpeciesAdd(species);
			} else {
				_species.Remove((Species)list.SelectedItem);
			}
		} catch (DataStoreConflictChangedException changed) {
			var conflictPage = new ConflictPage(species, changed.ConflicObject);

			((Species)list.SelectedItem).CopyFrom(changed.ConflicObject);
			await Refresh();
            conflictPage.OnDone += ConflictPage_OneDone;

			await Navigation.PushAsync(conflictPage);
		}
	}

	private async void ConflictPage_OneDone(object sender, Species species) {
		var newSpecies = await DataStore.SpeciesUpdate(species, true);
		((Species)list.SelectedItem).CopyFrom(newSpecies);
		await Refresh();
    }

	private async Task SpeciesNew(Species species) {
		var newSpecies = await DataStore.SpeciesAdd(species);
		_species.Add(newSpecies);
	}

	private async void BtnDelete_Clicked(object sender, EventArgs e) {
		var species = (Species)((Button)sender).Parent.Parent.BindingContext;
		try {
			await DataStore.SpeciesRemove(species);
			_species.Remove(species);
            await Refresh();
        }
        catch (DataStoreConflictChangedException deleted) {
			var popUp = new MsgPopUp($"Species {species.Name} has change, do you still want to delete?") {
				CanBeDismissedByTappingOutsideOfPopup = false
			};

			var shouldDelete = (bool)await this.ShowPopupAsync(popUp);

			if (shouldDelete) {
				await DataStore.SpeciesRemove(species, true);
				_species.Remove(species);
			}
			
			await Refresh();
		}
	}

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
		await Refresh();
    }

	private void BtnLogout_Clicked(object sender, EventArgs e) {
		((App)this.Parent.Parent.Parent).Logout();
	}
}
