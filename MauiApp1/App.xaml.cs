namespace MauiApp1;
using MauiApp1.Services;

public partial class App : Application
{
    public App(MainPage mainPage)
	{
		InitializeComponent();
        var navPage = new NavigationPage(mainPage);
        MainPage = navPage;
    }
}
