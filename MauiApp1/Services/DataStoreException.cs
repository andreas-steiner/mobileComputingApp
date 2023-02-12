using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services;

public class DataStoreException : Exception {
	public DataStoreException(string message = null, Exception innerException = null) : base(message, innerException) {  }
}

public class DataStoreCancleException : DataStoreException {
	public DataStoreCancleException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictException<T> : DataStoreException where T : ChangedStateModel {
	public bool IsDeleted { get; set; }
	public bool IsChanged { get; set; }

	public T ConflicObject { get; set; }

	public static DataStoreConflictException<T> AsChanged(T newValue = null)
		=> new DataStoreConflictException<T>() { IsDeleted = true, ConflicObject = newValue };

	public static DataStoreConflictException<T> AsDeleted()
		=> new DataStoreConflictException<T>() { IsChanged = true };
}
