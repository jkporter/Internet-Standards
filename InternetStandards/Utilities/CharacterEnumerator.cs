using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace InternetStandards.Utilities
{
    public class CharacterEnumerator : IEnumerator<string>
    {
        private readonly string _s;
        private int _index = -1;

        public CharacterEnumerator(string s)
        {
            _s = s;
        }

        public int GetCodePoint()
        {
            return char.ConvertToUtf32(_s, _index);
        }

        public string Current { get; private set; }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            var moveAhead = _index == -1 ? 1 : Current.Length;
            if (_index + moveAhead >= _s.Length)
                return false;

            Current = _s.Substring((_index += moveAhead), char.IsSurrogatePair(_s, _index) ? 2 : 1);

            return true;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}
