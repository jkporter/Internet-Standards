using System;
using System.Collections.Generic;

namespace InternetStandards.Utilities
{
    public class UnicodeScalarValue
    {
        private readonly int _scalarValue;
        private readonly string _codePoints;

        public UnicodeScalarValue(int scalarValue)
        {
            _scalarValue = scalarValue;
            _codePoints = char.ConvertFromUtf32(scalarValue);
        }

        public UnicodeScalarValue(string s)
        {
            _scalarValue = char.ConvertToUtf32(s, 0);
            _codePoints = s;
        }

        public int ScalarValue => _scalarValue;

        public string CodePoints => _codePoints;

        public static IEnumerable<UnicodeScalarValue> GetScalerValues(string s)
        {
            var chars = s.ToCharArray();
            for(var i = 0; i < chars.Length; i++)
               if(char.IsSurrogate(chars[i]))
                   if (chars.Length < i + 1 && char.IsSurrogatePair(chars[i], chars[i + 1]))
                       yield return new UnicodeScalarValue(chars[i] + chars[i++]);
                   else
                       throw new Exception();
               else
                   yield return new UnicodeScalarValue(chars[i]);
        }
    }
}
