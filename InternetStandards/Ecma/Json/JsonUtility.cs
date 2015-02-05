using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternetStandards.Ecma.EcmaScript;

namespace InternetStandards.Ecma.Json
{
    public static class JsonUtility
    {
        public static string ToJsonString(string s)
        {
            if (s == null)
                return "null";

            var sb = new StringBuilder();
            foreach (var c in s)

                // %x20-21 / %x23-5B / %x5D-10FFFF unescaped

                if(char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.Control)
                    switch (c)
                    {
                        case '"':
                            sb.Append(@"\""");
                            break;
                        case '\\':
                            sb.Append(@"\""");
                            break;
                        case '/':
                            sb.Append(@"\/");
                            break;
                        case '\u007F':
                            sb.Append(@"\b");
                            break;
                        case '\u000C':
                            sb.Append(@"\f");
                            break;
                        case '\u000A':
                            sb.Append(@"\n");
                            break;
                        case '\u000D':
                            sb.Append(@"\r");
                            break;
                        case '\u0009':
                            sb.Append(@"\t");
                            break;
                        default:
                            sb.Append(@"\u" + ((short)c).ToString("X4"));
                            break;
                    }
                else
                    sb.Append(c);

            return '"' + sb.ToString() + '"';
        }
    }
}
