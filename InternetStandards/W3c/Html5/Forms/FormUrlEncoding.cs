using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using InternetStandards.Utilities.Collections.Specialized;
using System.Globalization;
using InternetStandards.Utilities;
using InternetStandards.Utilities.Collections.Generic;
using System.Linq;
using System.IO;

namespace InternetStandards.W3c.Html5.Forms
{
    public class FormUrlEncoding
    {
        public static Stream Encode(FormDataSet formDataSet, Encoding[] supportedEncodings, string acceptCharset, Encoding documentEncoding)
        {
            var result = new StringBuilder();

            var selectedEncoding = SelectEncoding(formDataSet, supportedEncodings, acceptCharset, documentEncoding);

            var charset = GetPreferredMimeName(selectedEncoding);

            var entryIndex = -1;
            foreach (var entry in formDataSet)
                SubSteps(entry, entryIndex++, selectedEncoding, ref result);

            return new MemoryStream(Encoding.ASCII.GetBytes(result.ToString()));
        }

        private static void SubSteps(FormEntry entry, int entryIndex, Encoding selectedEncoding, ref StringBuilder result)
        {
            if (entry.Name == " charset " & entry.Type == "hidden")
                entry.Value = "charset";

            /* if (entry.Type == "file")
                entry.Value = entry.Name; */

            // Replace charachers

            if (entry.Name == "isindex" && entry.Type == "text" && entryIndex == 0)
            {
                result.Append(entry.Value);
                return;
            }

            if (entryIndex != 0)
                result.Append('&');

            result.Append(entry.Name);
            result.Append('=');
            result.Append(entry.Value);
        }

        private static void SubSubSteps(string input, Encoding selectedEncoding, ref StringBuilder result)
        {
            var enumerator = new CharacterEnumerator(input);

            while (enumerator.MoveNext())
            {
                var c = enumerator.Current;
                var codePoint = enumerator.GetCodePoint();

                if (c == " ")
                {
                    result.Append('+');
                }
                else if (!Regex.IsMatch(c, "[\u0020\u002A\u002D\u002E\u0030-\u0039\u0041-\u005A\u005F\u0061-\007A]"))
                {
                    string s = string.Empty;
                    foreach (var b in selectedEncoding.GetBytes(s))
                        SubSubSubStep(b, ref s);
                }
                else
                {
                    result.Append(c);
                }
            }
        }

        private static void SubSubSubStep(byte b, ref string s)
        {
            if (b == 0)
                s += (char)b;
            else
                s += "%" + b.ToString("X2");
        }

        private static Encoding SelectEncoding(FormDataSet formDataSet, IEnumerable<Encoding> supportedEncodings, string acceptCharset, Encoding documentEncoding)
        {
            if (acceptCharset != null)
            {
                string[] tokens = Html5Utility.SplitStringOnSpaces(acceptCharset);

                var encodings = tokens.Select(Encoding.GetEncoding)
                    .Where(e => e != null)
                    .Where(e => supportedEncodings == null || supportedEncodings.Any(se => e.GetType() == se.GetType()))
                    .Where(IsAsciiCompatible);

                return encodings.FirstOrDefault() ?? Encoding.UTF8;
            }
            else if (IsAsciiCompatible(documentEncoding))
            {
                return documentEncoding;
            }
            else
            {
                return Encoding.UTF8;
            }
        }

        public static bool IsAsciiCompatible(Encoding e)
        {
            var bytes = new byte[] { 0x09, 0x0A, 0x0C, 0x0D, 0x20, 0x21, 0x22, 0x26, 0x27, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A };

            e = (Encoding)e.Clone();
            var asciiEncoding = (Encoding)Encoding.ASCII.Clone();
            
            e.DecoderFallback = asciiEncoding.DecoderFallback = new DecoderExceptionFallback();

            try
            {
                return e.GetChars(bytes).SequenceEqual(Encoding.ASCII.GetChars(bytes));
            }
            catch(DecoderFallbackException)
            {
                return false;
            }
        }

        private static string GetPreferredMimeName(Encoding e)
        {
            return e.HeaderName;
        }
    }
}
