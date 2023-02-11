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
    
    //public async Task click()
    //{
    //    await Navigation.PushAsync(new DetailPage(dataStore, ...));
    //}
}

