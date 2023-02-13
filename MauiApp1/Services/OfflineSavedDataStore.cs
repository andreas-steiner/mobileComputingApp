using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services;
public class OfflineSavedDataStore : DataStore {
	private readonly DataStore _dataStore;

	public OfflineSavedDataStore(DataStore dataStore) {
		_dataStore = dataStore;
	}

	public Task<List<Species>> AllSpecies() {
		try {
			_dataStore.AllSpecies();

		} catch (DataStoreException) {
			// Exception vom server
			throw;
		} catch (Exception) {
			// Excepiton wenn internet nicht geht
			return SecureStorage.Default.GetAsync
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
