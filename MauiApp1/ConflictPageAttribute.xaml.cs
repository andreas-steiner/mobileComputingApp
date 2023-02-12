using MauiApp1.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ConflictPageAttribute : ContentPage
{
    private readonly DataStore dataStore;
    private readonly Species species;
    public ConflictPageAttribute()
	{
		InitializeComponent();
		var PhoneString = "1";
		var ServerStrig = "2";
	}
}