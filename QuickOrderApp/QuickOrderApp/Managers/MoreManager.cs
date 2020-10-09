using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOrderApp.Managers
{
    public  class MoreManager<T>
    {
        public Dictionary<string, IEnumerable<T>> DataValues { get; set; }

        public MoreManager()
        {
            DataValues = new Dictionary<string, IEnumerable<T>>();
        }

        public  bool AddKeyAndValues(string key, IEnumerable<T> values)
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

        //public  IEnumerable<T>  InsertDifferentDataValue(IEnumerable<T> newvalue)
        //{

        //}

    }
}
