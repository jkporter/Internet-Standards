using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using InternetStandards.Utilities.Collections.Specialized;
using System.Xml;
using InternetStandards.Utilities;
using Microsoft.VisualBasic.ApplicationServices;

namespace InternetStandards.W3c.XForms;

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

    private static string InternalEncode(string str)
    {
        var sb = new StringBuilder();
        var enumerator = new CharacterEnumerator(str);

        var previousCodePoint = -1;
        while (enumerator.MoveNext())
        {
            var codePoint = enumerator.GetCodePoint();
            switch (codePoint)
            {
                case ' ':
                    sb.Append('+');
                    break;
                case '\u2028' or '\u0085' or '\u000D':
                case '\u000A' when previousCodePoint != '\u000D':
                    sb.Append("%0D%0A");
                    break;
                case '\u000A':
                    break;
                case <= 127 when ":/?#[]@!$&'()*+,;=".Contains(enumerator.Current!):
                case > 127:
                    foreach (var b in Encoding.UTF8.GetBytes(enumerator.Current!))
                    {
                        sb.Append('%');
                        sb.Append(b.ToString("X2"));
                    }
                    break;
                default:
                    sb.Append(enumerator.Current!);
                    break;
            }

            previousCodePoint = codePoint;
        }

        return sb.ToString();
    }


}