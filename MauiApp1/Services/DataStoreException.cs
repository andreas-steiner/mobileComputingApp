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
public class DataStoreConflictException : DataStoreException {
	public DataStoreConflictException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictDeletedException : DataStoreConflictException {
	public DataStoreConflictDeletedException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictChanedException<T> : DataStoreConflictException where T : ChangedStateModel {
	public DataStoreConflictChanedException(T newValue, string message = null, Exception innerException = null) : base(message, innerException) {
		ConflicObject = newValue;
	}

	public T ConflicObject { get; set; }
}
