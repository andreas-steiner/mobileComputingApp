using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services;
public class DataStoreConflictException<T> : Exception where T : class {
	public bool IsDeleted { get; set; }
	public bool IsChanged { get; set; }

	public T ConflicObject { get; set; }

	public static DataStoreConflictException<T> AsChanged(T newValue = null)
		=> new DataStoreConflictException<T>() { IsDeleted = true, ConflicObject = newValue };

	public static DataStoreConflictException<object> AsDeleted()
		=> new DataStoreConflictException<object>() { IsChanged = true };
}
