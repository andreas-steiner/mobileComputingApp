using Shared;
using System.Net;
using System.Net.Http.Json;

namespace MauiApp1.Services;
public class WebDataStore : DataStore {
	private readonly HttpClient _http;

	public WebDataStore(HttpClient http) {
		_http = http;
		_http.BaseAddress = new Uri("https://mc.schertmi.net/api/Species");
	}

	public Task<List<Species>> AllSpecies()
		=> _http.GetFromJsonAsync<List<Species>>("");

	public Task<Species> SpeciesGet(int id)
		=> _http.GetFromJsonAsync<Species>($"{id}");

	public async Task<Species> SpeciesAdd(Species species) {
		var resp = await _http.PostAsJsonAsync("", species);
		return await resp.Content.ReadFromJsonAsync<Species>();
	}

	public async Task<Species> SpeciesUpdate(Species species, bool force = false) {
		if (force) {
			_http.DefaultRequestHeaders.Add("Force", "");
		}

		var resp = await _http.PutAsJsonAsync("", species);

		if (force) {
			_http.DefaultRequestHeaders.Remove("Force");
		}

		if (resp.StatusCode == HttpStatusCode.NoContent) {
			throw new DataStoreConflictDeletedException();
		}

		if (resp.StatusCode != HttpStatusCode.Conflict && !resp.IsSuccessStatusCode) {
			throw new DataStoreConflictException();
		}

		var serverSpecies = await resp.Content.ReadFromJsonAsync<Species>();

		if (resp.StatusCode == HttpStatusCode.Conflict) {
			throw new DataStoreConflictChangedException(serverSpecies);
		}

		return serverSpecies;
	}

	public async Task SpeciesRemove(Species species, bool force = false) {
		if (force) {
			_http.DefaultRequestHeaders.Add("Force", "");
		}

		var time = species.LastEdited.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
		var resp = await _http.DeleteAsync($"{species.Id}?lastEdited={time}");

		if (force) {
			_http.DefaultRequestHeaders.Remove("Force");
		}

		if (resp.StatusCode == HttpStatusCode.Conflict) {
			var serverSpecies = await resp.Content.ReadFromJsonAsync<Species>();
			throw new DataStoreConflictChangedException(serverSpecies);
		}

		if (!resp.IsSuccessStatusCode) {
			throw new DataStoreConflictException();
		}
	}
}
