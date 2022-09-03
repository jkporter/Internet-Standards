using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using InternetStandards.Utilities.Collections.Specialized;
using InternetStandards.Utilities;

namespace InternetStandards.W3c.Html
{
    public class FormUrlEncoding
    {
        public static NamedValueList<string> Decode(string formUrlEncodedString, string newLine = "\r\n")
        {
            const string encodedControlNameOrValuePattern = "((%[A-Fa-f0-9]{2})|[^%&=])*)";
            const string encodedControlNameAndValuePattern = "(?<nameAndValue>(?<name>" + encodedControlNameOrValuePattern + ")=(?<value>" + encodedControlNameOrValuePattern + "))";
            const string encodedPattern = "^(" + encodedControlNameAndValuePattern + "(&" + encodedControlNameAndValuePattern + ")*)?$";

            var encodedRegEx = new Regex(encodedPattern);
            var match = encodedRegEx.Match(formUrlEncodedString);

            if (!match.Success)
                throw new FormatException(
                    $"The string '{formUrlEncodedString}' is not a valid form URL encoded string.");

            var controlNameValuePairs = new NamedValueList<string>();
            foreach (var controlNameValuePair in formUrlEncodedString.Split('&').Select(controlNameValuePairString => controlNameValuePairString.Split('=')))
            {
                controlNameValuePairs.Add(
                    InternalDecode(controlNameValuePair[0], newLine),
                    InternalDecode(controlNameValuePair[1], newLine));
            }

            return controlNameValuePairs;
        }

        private static string InternalDecode(string escapedString, string newLine)
        {
            const string replacePattern = @"(?<space>\+)|(?<lineBreak>(?i)%0D%0A(?-i))|(?<nonAlphaNumeric>%([0-9a-zA-Z]{2}))|.";
            return Regex.Replace(escapedString, replacePattern, new FormUrlEncodingDecodeMatchEvaluation { NewLine = newLine }.MatchEvaluator);
        }

        private class FormUrlEncodingDecodeMatchEvaluation
        {
            private readonly ASCIIEncoding asciiEncoding = new();

            public FormUrlEncodingDecodeMatchEvaluation()
            {
                NewLine = "\r\n";
            }

            public string NewLine
            {
                get;
                set;
            }

            public string MatchEvaluator(Match m)
            {
                if (m.Groups["space"].Success)
                    return " ";

                if (m.Groups["lineBreak"].Success)
                    return "\u2028";

                if (!m.Groups["nonAlphaNumeric"].Success) return m.Value;

                var value = asciiEncoding.GetString(new[] { Convert.ToByte(m.Groups["nonAlphaNumeric"].Value[1..], 16) })[0];
                if (char.IsLetterOrDigit(value))
                    throw new FormatException(
                        $"'{m.Groups["nonAlphaNumeric"].Value}' decodes to an alpha-numeric character and should not have been escaped.");
                if (value == 0x0A || value == 0x0D)
                    return NewLine ?? value.ToString();
                return value.ToString();
            }
        }

        public static string Encode(NamedValueList<string> controls)
        {
            var sb = new StringBuilder();
            var firstControl = true;
            foreach (var keyValuePair in controls)
            {
                var value = keyValuePair.Value;
                if (!firstControl) sb.Append('&');
                sb.Append(InternalEncode(keyValuePair.Name, value));
                firstControl = false;
            }

            return sb.ToString();
        }

        private static string InternalEncode(string name, string value)
        {
            return InternalEncode(name) + '=' + InternalEncode(value);
        }

        private static string InternalEncode(string str)
        {
            var sb = new StringBuilder();
            var enumerator = new CharacterEnumerator(str);

            var lastWasCr = false;
            while (enumerator.MoveNext())
            {
                var codePoint = enumerator.GetCodePoint();

                switch (codePoint)
                {
                    case ' ':
                        sb.Append('+');
                        break;
                    case '\u2029' or '\u2028' or '\u0085' or '\u000D':
                    case '\u000A' when !lastWasCr:
                        sb.Append("%0D%0A");
                        break;
                    case '\u000A':
                        break;
                    case >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9':
                        sb.Append(enumerator.Current);
                        break;
                    case > (char)127:
                        throw new FormatException(
                            $"The non-alphanumeric character '{enumerator.Current}' cannot be represented as ASCII coded character.");
                    default:
                        sb.Append("%" + ((byte)codePoint).ToString("X2"));
                        break;
                }

                lastWasCr = codePoint == '\u000D';
            }

            return sb.ToString();
        }
    }
}
