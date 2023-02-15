namespace MauiApp1;
using MauiApp1.Services;
using Microsoft.Maui;

public partial class App : Application
{
	private readonly Page _loginPage;
	private readonly DataStore _dataStore;
	private readonly HttpClient _http;

	public App(DataStore dataStore, HttpClient http)
	{
		InitializeComponent();

		var loginBtn = new Button() { Text = "Login" };
		loginBtn.Clicked += LoginClick;
		_loginPage = new ContentPage() {
			Content = new StackLayout() {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = { loginBtn }
			}
		};

		MainPage = _loginPage;

		_dataStore = dataStore;
		_http = http;
	}

	protected async override void OnStart() {
		base.OnStart();

		var idToken = await SecureStorage.GetAsync("id_token");

		if (idToken is not null) {
			SetMainPage(idToken);
			return;
		}
	}

	async void LoginClick(object sender, EventArgs e) {
		await StartAuth();
	}

	private async Task StartAuth() {
		try {
			WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
				new Uri("https://login.schertmi.net/application/o/authorize/?client_id=cb907409c33677dd7c4db9bd9c62a33187f84316&redirect_uri=mcapp://&scope=openid profile email&response_type=id_token&state=0"),
				new Uri("mcapp://"));

			string token = authResult?.IdToken;
			await SecureStorage.SetAsync("id_token", token);
			SetMainPage(token);
		} catch (TaskCanceledException e) {
			MainPage = new ContentPage() {
				Content = new StackLayout() {
					new Label() { Text = "Error while login" }
				}
			};
		}
	}
	
	private void SetMainPage(string idToken) {
		_dataStore.SetIdToken(idToken);
		var mainPage = new MainPage(_dataStore);
		
		var navPage = new NavigationPage(mainPage);
		MainPage = navPage;
	}

	public void Logout() {
		SecureStorage.Remove("id_token");
		_http.DefaultRequestHeaders.Authorization = null;
		MainPage = _loginPage;
	}
}
