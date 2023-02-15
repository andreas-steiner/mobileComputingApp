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

public class DataStoreNoAuthException : DataStoreException {
	public DataStoreNoAuthException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictException : DataStoreException {
	public DataStoreConflictException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictDeletedException : DataStoreConflictException {
	public DataStoreConflictDeletedException(string message = null, Exception innerException = null) : base(message, innerException) { }
}

public class DataStoreConflictChangedException : DataStoreConflictException {
	public DataStoreConflictChangedException(Species newValue, string message = null, Exception innerException = null) : base(message, innerException) {
		ConflicObject = newValue;
	}

	public Species ConflicObject { get; set; }
}
