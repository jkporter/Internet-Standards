using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.Serialization;
using InternetStandards.Utilities.Collections.Generic;

namespace InternetStandards.Utilities.Collections.Specialized
{
    public class NamedValueList<TValue> : List<NameValuePair<TValue>>
    {
        public NamedValueList(IEnumerable<NameValuePair<TValue>> collection)
            : base(collection)
        {
        }

        public NamedValueList(int capacity)
            : base(capacity)
        {
        }

        public NamedValueList()
            : base()
        {
        }

        public virtual void Add(string name, TValue value)
        {
            this.Add(new NameValuePair<TValue>(name, value));
        }

        public virtual bool ContainsKey(string key)
        {
            return GetIndexByName(key) != -1;
        }

        public virtual bool ContainsValue(TValue value)
        {
            return GetIndex(value) != -1;
        }

        public virtual int GetIndex(TValue value)
        {
            var equalatableObject = value as IEqualityComparer<TValue>;
            if (value != null)
            {
                for (int i = 0; i < this.Count; i++)
                    if (equalatableObject.Equals(this[i].Value))
                        return i;
            }

            return -1;
        }

        public virtual int GetIndexByName(string name)
        {
            for (int i = 0; i < this.Count; i++)
                if (this[i].Name == name)
                    return i;

            return -1;
        }

        public virtual string[] GetUniqueKeys()
        {
            List<string> keys = new List<string>();
            foreach (NameValuePair<TValue> nameValuePair in this)
                if (!keys.Contains(nameValuePair.Name))
                    keys.Add(nameValuePair.Name);

            if (keys.Count == 0)
                return null;

            return keys.ToArray();
        }

        public virtual string[] GetKeys()
        {
            List<string> names = new List<string>();
            foreach (NameValuePair<TValue> nameValuePair in this)
                names.Add(nameValuePair.Name);

            if (names.Count == 0)
                return null;

            return names.ToArray();
        }

        public virtual TValue[] GetValues(string name)
        {
            List<TValue> values = new List<TValue>();
            foreach (NameValuePair<TValue> nameValuePair in this)
                if (nameValuePair.Name == name)
                    values.Add(nameValuePair.Value);

            if (values.Count == 0)
                return null;

            return values.ToArray();
        }

        public virtual string[] Keys
        {
            get
            {
                return GetKeys();
            }
        }

        public virtual TValue[] this[string key]
        {
            get
            {
                return GetValues(key);
            }
        }
    }
}
