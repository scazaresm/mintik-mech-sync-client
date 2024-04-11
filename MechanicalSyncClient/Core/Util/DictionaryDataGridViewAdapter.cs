using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechanicalSyncApp.Core.Util
{
    public class DictionaryDataGridViewAdapter : IDictionary<string, DataGridViewRow>
    {
        private readonly Dictionary<string, DataGridViewRow> innerDictionary = new Dictionary<string, DataGridViewRow>();
        private readonly DataGridViewRowCollection rowCollection;

        private readonly object lockObject = new object();

        public DictionaryDataGridViewAdapter(DataGridViewRowCollection rowCollection)
        {
            this.rowCollection = rowCollection;
        }

        public void Add(string key, DataGridViewRow value)
        {
            lock (lockObject)
            {
                innerDictionary.Add(key, value);
                rowCollection.Add(value);
            }
        }

        public bool Remove(string key)
        {
            lock (lockObject)
            {
                if (innerDictionary.TryGetValue(key, out DataGridViewRow value))
                {
                    innerDictionary.Remove(key);
                    rowCollection.Remove(value);
                    return true;
                }
                return false;
            }
        }

        public void Clear()
        {
            lock (lockObject)
            {
                innerDictionary.Clear();
                rowCollection.Clear();
            }
        }

        public DataGridViewRow this[string key]
        {
            get => innerDictionary[key];
            set
            {
                lock (lockObject)
                {
                    if (innerDictionary.TryGetValue(key, out DataGridViewRow existingValue))
                    {
                        rowCollection.Remove(existingValue);
                    }
                    innerDictionary[key] = value;
                    rowCollection.Add(value);
                }
            }
        }

        public int Count => innerDictionary.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => innerDictionary.Keys;

        public ICollection<DataGridViewRow> Values => innerDictionary.Values;

        public bool ContainsKey(string key) => innerDictionary.ContainsKey(key);

        public bool TryGetValue(string key, out DataGridViewRow value) => innerDictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, DataGridViewRow>> GetEnumerator() => innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<KeyValuePair<string, DataGridViewRow>>.Add(KeyValuePair<string, DataGridViewRow> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<string, DataGridViewRow>>.Contains(KeyValuePair<string, DataGridViewRow> item)
        {
            return ((ICollection<KeyValuePair<string, DataGridViewRow>>)innerDictionary).Contains(item);
        }

        void ICollection<KeyValuePair<string, DataGridViewRow>>.CopyTo(KeyValuePair<string, DataGridViewRow>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, DataGridViewRow>>)innerDictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, DataGridViewRow>>.Remove(KeyValuePair<string, DataGridViewRow> item)
        {
            if (((ICollection<KeyValuePair<string, DataGridViewRow>>)innerDictionary).Remove(item))
            {
                DataGridViewRow removedValue = item.Value;
                rowCollection.Remove(removedValue);
                return true;
            }
            return false;
        }
    }
}
