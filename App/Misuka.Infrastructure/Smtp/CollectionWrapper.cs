using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class CollectionWrapper
    {
        readonly IDictionary<string, object> _collection = new Dictionary<string, object>();

        internal static CollectionWrapper GetSerializeableCollection(NameValueCollection col)
        {
            if (col == null)
                return null;

            CollectionWrapper scol = new CollectionWrapper();
            foreach (String key in col.Keys)
                scol._collection.Add(key, col[key]);

            return scol;
        }

        internal static CollectionWrapper GetSerializeableCollection(StringDictionary col)
        {
            if (col == null)
                return null;

            CollectionWrapper scol = new CollectionWrapper();
            foreach (String key in col.Keys)
                scol._collection.Add(key, col[key]);

            return scol;
        }

        internal void SetColletion(NameValueCollection scol)
        {
            foreach (String key in _collection.Keys)
                scol.Add(key, (string)_collection[key]);
        }

        internal void SetColletion(StringDictionary scol)
        {
            foreach (String key in _collection.Keys)
            {
                if (scol.ContainsKey(key))
                    scol[key] = (string)_collection[key];
                else
                    scol.Add(key, (string)_collection[key]);
            }
        }
    }
}