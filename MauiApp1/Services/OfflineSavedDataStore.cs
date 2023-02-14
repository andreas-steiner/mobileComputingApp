using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.IO.Enumeration;

namespace MauiApp1.Services;
public class OfflineSavedDataStore : DataStore {
	private readonly DataStore _dataStore;

	public OfflineSavedDataStore(DataStore dataStore) {
		_dataStore = dataStore;
	}

	public Task<List<Species>> AllSpecies() {
        var path = FileSystem.Current.AppDataDirectory;
        string fileName = "OfflineData";
        var fullPath = Path.Combine(path, fileName);
        try {
			_dataStore.AllSpecies();
            var serializedData = JsonSerializer.Serialize(_dataStore.AllSpecies());
            File.WriteAllText(fullPath, serializedData);

		} catch (DataStoreException) {
			// Exception vom server
			throw;
		} catch (Exception) {
            // Excepiton wenn internet nicht geht
			// TODO
			// Retrieve Data from local storage
            var rawData = File.ReadAllText(fullPath);
			List<Species> SpeciesOffline = JsonSerializer.Deserialize<List<Species>>(rawData);
			return null; //return read Data
			throw;
		}

		return new List<Species>();
	}

	public Task<Species> SpeciesAdd(Species Species) {
		throw new NotImplementedException();
	}

	public Task<Species> SpeciesGet(int id) {
		throw new NotImplementedException();
	}

	public Task SpeciesRemove(Species species, bool force = false) {
		throw new NotImplementedException();
	}

	public Task<Species> SpeciesUpdate(Species species, bool force = false) {
		throw new NotImplementedException();
	}
}
