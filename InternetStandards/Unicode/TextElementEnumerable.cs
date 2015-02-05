using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace InternetStandards.Unicode
{
    public class TextElementEnumerable : IEnumerable<string>
    {
        private readonly string _str;

        public TextElementEnumerable(string str)
        {
            _str = str;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new TextElementEnumeratorWrapper(StringInfo.GetTextElementEnumerator(_str));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
