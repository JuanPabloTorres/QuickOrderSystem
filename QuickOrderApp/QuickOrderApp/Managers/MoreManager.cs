using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickOrderApp.Managers
{
	public class MoreManager<T>
	{
		public Dictionary<string, IEnumerable<T>> DataValues { get; set; }

		public MoreManager()
		{
			DataValues = new Dictionary<string, IEnumerable<T>>();
		}

		public bool ExistKey(string name)
		{
			if (DataValues.ContainsKey(name))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool AddKeyAndValues(string key, IEnumerable<T> values)
		{
			if (!DataValues.ContainsKey(key))
			{
				DataValues.Add(key, values);
				return true;
			}
			else
			{
				return false;
			}
		}


		public IEnumerable<T> GetValues(string key)
		{
			if (ExistKey(key))
			{
				return DataValues[key];
			}
			else
			{
				return null;
			}
		}

		public bool ModifyDictionary(string key, IEnumerable<T> newValues)
		{
			if (DataValues.ContainsKey(key))
			{
				DataValues.Clear();
				DataValues.Add(key, newValues);
				return true;
			}
			else
			{
				return false;
			}
		}

		public IEnumerable<T> InsertDifferentDataValue(IEnumerable<T> newvalue, string key)
		{
			List<T> tempData = new List<T>();

			foreach (var item in DataValues[key])
			{


				tempData.Add(item);

			}

			foreach (var item in newvalue)
			{
				tempData.Add(item);
			}

			return tempData.ToList();


		}

	}
}
