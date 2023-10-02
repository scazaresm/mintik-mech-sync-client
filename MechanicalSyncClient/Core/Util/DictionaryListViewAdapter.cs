using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace MechanicalSyncApp.Core.Util
{
    public class DictionaryListViewAdapter : IDictionary<string, ListViewItem>
    {
        private readonly Dictionary<string, ListViewItem> innerDictionary = new Dictionary<string, ListViewItem>();
        private readonly ListViewItemCollection itemCollection;

        private readonly object lockObject = new object();

        public DictionaryListViewAdapter(ListViewItemCollection itemCollection)
        {
            this.itemCollection = itemCollection;
        }

        public void Add(string key, ListViewItem value)
        {
            lock (lockObject) 
            {
                innerDictionary.Add(key, value);
                itemCollection.Add(value);
            }
        }

        public bool Remove(string key)
        {
            lock (lockObject)
            {
                if (innerDictionary.TryGetValue(key, out ListViewItem value))
                {
                    innerDictionary.Remove(key);
                    itemCollection.Remove(value);
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
                itemCollection.Clear();
            }
        }

        public ListViewItem this[string key]
        {
            get => innerDictionary[key];
            set
            {
                lock (lockObject)
                {
                    if (innerDictionary.TryGetValue(key, out ListViewItem existingValue))
                    {
                        itemCollection.Remove(existingValue);
                    }
                    innerDictionary[key] = value;
                    itemCollection.Add(value);
                }
            }
        }

        public int Count => innerDictionary.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => innerDictionary.Keys;

        public ICollection<ListViewItem> Values => innerDictionary.Values;

        public bool ContainsKey(string key) => innerDictionary.ContainsKey(key);

        public bool TryGetValue(string key, out ListViewItem value) => innerDictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, ListViewItem>> GetEnumerator() => innerDictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<KeyValuePair<string, ListViewItem>>.Add(KeyValuePair<string, ListViewItem> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<string, ListViewItem>>.Contains(KeyValuePair<string, ListViewItem> item)
        {
            return ((ICollection<KeyValuePair<string, ListViewItem>>)innerDictionary).Contains(item);
        }

        void ICollection<KeyValuePair<string, ListViewItem>>.CopyTo(KeyValuePair<string, ListViewItem>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, ListViewItem>>)innerDictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, ListViewItem>>.Remove(KeyValuePair<string, ListViewItem> item)
        {
            if (((ICollection<KeyValuePair<string, ListViewItem>>)innerDictionary).Remove(item))
            {
                ListViewItem removedValue = item.Value;
                itemCollection.Remove(removedValue);
                return true;
            }
            return false;
        }
    }
}
