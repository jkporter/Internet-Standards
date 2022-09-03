using System;
using System.Globalization;
using System.Runtime.InteropServices;
using InternetStandards.Utilities;

namespace InternetStandards.Unicode
{
    [SerializableAttribute]
    [ComVisible(true)]
    public class TextElementEnumeratorWrapper : GenericEnumerator<string>
    {
        internal TextElementEnumeratorWrapper(TextElementEnumerator textElementEnumerator)
            : base(textElementEnumerator)
        {
        }

        public string GetTextElement()
        {
            return ((TextElementEnumerator)BaseEnumerator).GetTextElement();
        }

        public int ElementIndex => ((TextElementEnumerator)BaseEnumerator).ElementIndex;
    }
}
