using System;
using System.Collections;
using System.Collections.Generic;

namespace InternetStandards.Utilities
{
    public class CharacterEnumerator : IEnumerator<string>
    {
        private readonly string _s;
        private int _currentIndex = -1;
        private bool currentIsSurrogatePair;
        private bool hasNext = true;

        public CharacterEnumerator(string s)
        {
            _s = s;
        }

        public int GetCodePoint()
        {
            if(_currentIndex != -1 && hasNext)
                return char.ConvertToUtf32(_s, _currentIndex);

            throw new InvalidOperationException();
        }

        public string Current { get; private set; }

        public void Dispose()
        {
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (!hasNext || (hasNext = (_currentIndex += currentIsSurrogatePair ? 2 : 1) >= _s.Length))
                return false;

            Current = _s.Substring(_currentIndex,
                (currentIsSurrogatePair = char.IsSurrogatePair(_s, _currentIndex)) ? 2 : 1);

            return true;
        }

        public void Reset()
        {
            _currentIndex = -1;
            hasNext = true;
        }
    }
}
