using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Utilities.Collections
{
    [Serializable]
    public struct NamedValue<TValue>
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.KeyValuePair<TKey,TValue>
        //     structure with the specified key and value.
        //
        // Parameters:
        //   key:
        //     The object defined in each key/value pair.
        //
        //   value:
        //     The definition associated with key.
        public NamedValue(string name, TValue value)
        {
        }

        // Summary:
        //     Gets the key in the key/value pair.
        //
        // Returns:
        //     A TKey that is the key of the System.Collections.Generic.KeyValuePair<TKey,TValue>.
        public string Name { get; }
        //
        // Summary:
        //     Gets the value in the key/value pair.
        //
        // Returns:
        //     A TValue that is the value of the System.Collections.Generic.KeyValuePair<TKey,TValue>.
        public TValue Value { get; }

        // Summary:
        //     Returns a string representation of the System.Collections.Generic.KeyValuePair<TKey,TValue>,
        //     using the string representations of the key and value.
        //
        // Returns:
        //     A string representation of the System.Collections.Generic.KeyValuePair<TKey,TValue>,
        //     which includes the string representations of the key and value.
        public override string ToString();
    }
}
