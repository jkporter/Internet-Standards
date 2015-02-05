using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace InternetStandards.Ecma.EcmaScript
{
    public static class EmcaScriptUtility
    {
        public static string ToDoubleStringCharacters(string s)
        {
            return ToStringCharacters(s, EcmaScriptStringLiteralCharacters.DoubleString);
        }

        public static string ToSingleStringCharacters(string s)
        {
            return ToStringCharacters(s, EcmaScriptStringLiteralCharacters.SingleString);
        }

        private static string ToStringCharacters(string s, EcmaScriptStringLiteralCharacters stringLiteralStringCharacters)
        {
            char[] forbiddenCharacters = (((char)stringLiteralStringCharacters) + "\\\r\u2028\u2029\n").ToCharArray();

            var sb = new StringBuilder();
            foreach (var c in s)
                if (c == (char)stringLiteralStringCharacters)
                    sb.Append("\\" + (char)stringLiteralStringCharacters);
                else if (c == '\u000A')
                    sb.Append(@"\n");
                else if (c == '\u000D')
                    sb.Append(@"\r");
                else if (c == '\\')
                    sb.Append(@"\\");
                else if (forbiddenCharacters.Contains(c))
                    sb.Append(@"\u" + ((short)c).ToString("X4"));
                else
                    sb.Append(c);

            return sb.ToString();
        }

        public static string ToStringLiteral(string s, EcmaScriptStringLiteralCharacters characters)
        {
            if (characters == EcmaScriptStringLiteralCharacters.SingleString)
                return '\'' + ToSingleStringCharacters(s) + '\'';

            if(characters == EcmaScriptStringLiteralCharacters.DoubleString)
                return '"' + ToDoubleStringCharacters(s) + '"';

            throw new ArgumentException();
        }

        public enum EcmaScriptStringLiteralCharacters
        {
            DoubleString = '"',
            SingleString = '\''
        }
    }
}
