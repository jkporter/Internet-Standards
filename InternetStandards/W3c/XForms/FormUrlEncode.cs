using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using InternetStandards.Utilities.Collections.Specialized;
using System.Globalization;
using System.Xml;
using InternetStandards.Utilities;

namespace InternetStandards.W3c.XForms
{
    public static class FormUrlEncoding
    {
        public static NamedValueList<string> Decode(string formUrlEncodedString, string separator)
        {
            const string encodedControlNameOrValuePattern = "((%[A-Fa-f]{2})|[^%&=])*)";
            const string encodedControlNameAndValuePattern = "(?<nameAndValue>(?<name>" + encodedControlNameOrValuePattern + ")=(?<value>" + encodedControlNameOrValuePattern + "))";
            const string encodedPattern = "^(" + encodedControlNameAndValuePattern + "(&" + encodedControlNameAndValuePattern + ")*)?$";

            var encodedRegEx = new Regex(encodedPattern);
            var match = encodedRegEx.Match(formUrlEncodedString);

            if (!match.Success)
                throw new FormatException("The string '" + formUrlEncodedString + "' is not a valid form URL encoded string.");

            var controlNameValuePairs = new NamedValueList<string>();
            foreach (var controlNameValuePair in formUrlEncodedString.Split('&').Select(controlNameValuePairString => controlNameValuePairString.Split('=')))
            {
                controlNameValuePairs.Add(
                    InternalDecode(controlNameValuePair[0]),
                    InternalDecode(controlNameValuePair[1])
                    );
            }

            return controlNameValuePairs;
        }

        private static string InternalDecode(string escapedString)
        {
            var sb = new StringBuilder();
            var lastIsCr = false;
            for (var i = 0; i < escapedString.Length; i++)
            {
                var currentChar = escapedString[i];
                if (currentChar == '%')
                {
                    var decodedChar = (char)Convert.ToByte(escapedString.Substring(i++, 2));
                    i++;

                    if (lastIsCr && decodedChar != 10)
                        sb.Append('\u000D');

                    if (lastIsCr && decodedChar == 10)
                        sb.Append('\u2028');
                    else if (decodedChar != 13)
                        sb.Append(decodedChar);

                    lastIsCr = decodedChar == 13;
                }
                else
                {
                    sb.Append(currentChar == '+' ? ' ' : currentChar);
                    lastIsCr = false;
                }
            }

            if (lastIsCr)
                sb.Append('\u000D');

            return sb.ToString();
        }

        public static void Encode(XmlDocument document, TextWriter writer)
        {
            Encode(document, ";", writer);
        }

        public static void Encode(XmlDocument document, string separator, TextWriter writer)
        {
            var firstControl = true;
            foreach (XmlNode node in document.SelectNodes("//*[count(*) = 0]"))
            {
                if (!firstControl) writer.Write(separator);
                writer.Write(InternalEncode(node.LocalName, node.InnerText));
                firstControl = false;
            }
        }

        private static string InternalEncode(string eltName, string value)
        {
            return InternalEncode(eltName) + '=' + InternalEncode(value);
        }

        /* private static string InternalEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(str);

            bool lastWasCr = false;
            foreach (char c in str)
            {
                if (lastWasCr && c != '\u000A')
                    sb.Append("%0D%0A");

                if (c == ' ')
                    sb.Append('+');
                if (c == '\u2028' || c == '\u000A' || c == '\u0085')
                    sb.Append("%0D%0A");
                else if (c > 127 || ":/?#[]@!$&'()*+,;=".IndexOf(c) != -1)
                    foreach (byte b in Encoding.UTF8.GetBytes(new char[] {c}))
                    {
                        sb.Append("%");
                        sb.Append(b.ToString("X2"));
                    }
                else if (!(lastWasCr = c == '\u000D'))
                    sb.Append(c);
            }

            if (lastWasCr)
                sb.Append("%0D%0A");

            return sb.ToString();
        } */

        private static string InternalEncode(string str)
        {
            var sb = new StringBuilder();
            var enumerator = new CharacterEnumerator(str);

            var lastWasCr = false;
            while (enumerator.MoveNext())
            {
                var c = enumerator.Current;

                if (c == " ")
                    sb.Append('+');
                else if (c == "\u2028" || c == "\u0085" || c == "\u000D" || (c == "\u000A" && !lastWasCr))
                    sb.Append("%0D%0A");
                else if (enumerator.GetCodePoint() > 127 || ":/?#[]@!$&'()*+,;=".IndexOf(c, StringComparison.Ordinal) != -1)
                    foreach (var b in Encoding.UTF8.GetBytes(c))
                    {
                        sb.Append("%");
                        sb.Append(b.ToString("X2"));
                    }
                else if (c != "\u000A")
                    sb.Append(c);

                lastWasCr = c == "\u000D";
            }

            return sb.ToString();
        }
    }
}
