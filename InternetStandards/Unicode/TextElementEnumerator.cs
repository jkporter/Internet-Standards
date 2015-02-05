using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        public int ElementIndex {
            get { return ((TextElementEnumerator)BaseEnumerator).ElementIndex; }
        }
    }
}
