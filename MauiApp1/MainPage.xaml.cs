using MauiApp1.Services;
using Shared;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly DataStore dataStore;

    public MainPage(DataStore dataStore)
    {
        InitializeComponent();
        list.ItemsSource = dataStore.AllSpecies();
        this.dataStore = dataStore;
    }
    async void OnSpeciesTapped(object sender, ItemTappedEventArgs e)
    {
        var species = (Species)list.SelectedItem;
        await Navigation.PushAsync(new DetailPage(dataStore, species, false));
    }
    public async void OnAddClicked(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new DetailPage(dataStore, new Species(), true));
    }
}

