using System;
using System.Reflection;
using System.Collections.Generic;

namespace WSD
{
	public class ObjectHelper
	{
		static public object GetProperty(object obj, string propertyName, object defaultValue = null)
		{
			PropertyInfo property = obj.GetType ().GetRuntimeProperty (propertyName);

			return property != null ? (
				property.GetValue(obj, null) != null ? property.GetValue(obj, null) : defaultValue
			) : defaultValue;
		}

		static public Dictionary<string, object> GetProperties (object obj)
		{
			IEnumerable<PropertyInfo> properties = obj.GetType ().GetRuntimeProperties ();

			Dictionary<string, object> props = new Dictionary<string, object> ();

			var enumerator = properties.GetEnumerator ();

			while (enumerator.MoveNext ()) {
				PropertyInfo property = enumerator.Current;
				props.Add (property.Name, property.GetValue (obj));
			}

			return props;
		}

		static public Dictionary<string, T> GetProperties<T> (object obj)
		{
			Dictionary<string, object> _properties = GetProperties (obj);
			Dictionary<string, T> properties = new Dictionary<string, T> ();

			foreach (KeyValuePair<string, object> row in _properties) {
				properties.Add (row.Key, (T) row.Value);
			}

			return properties;
		}
	}
}