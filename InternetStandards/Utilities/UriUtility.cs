using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InternetStandards.Utilities
{
    public class UriUtility
    {
        public static string FilePathToFileUrl(string path, bool useLocalhost = false)
        {
            string host = useLocalhost ? "localhost" : string.Empty;
            string urlPath = null; 

            if (path.StartsWith(@"\\?\UNC\", StringComparison.CurrentCultureIgnoreCase))
            {
                int urlPathDelimiter = path.IndexOf('\\', 8);
                host = path.Substring(8, urlPathDelimiter - 8);
                urlPath = path.Substring(urlPathDelimiter);
            }
            else if (path.StartsWith(@"\\"))
            {
                int urlPathDelimiter = path.IndexOf('\\', 2);
                host = path.Substring(2, urlPathDelimiter - 2);
                urlPath = path.Substring(urlPathDelimiter);
            }
            else if (path.StartsWith(@"\\?\"))
            {
                urlPath = path.Substring(4);
            }
            else
            {
                urlPath = path;
            }

            char[] pathDelimiters = { '\\', '/' };

            StringBuilder sb = new StringBuilder("file://");
            sb.Append(host);
            foreach (string segment in urlPath.Split(pathDelimiters))
            {
                sb.Append('/');
                sb.Append(PathSegmentEncode(segment));
            }

            return sb.ToString();
        }

        public static string PathSegmentEncode(string str)
        {
            return PathSegmentEncode(str, Encoding.UTF8);
        }

        public static string PathSegmentEncode(string str, Encoding e)
        {
            StringBuilder pathEncoded = new StringBuilder();
            CharacterEnumerator characters = new CharacterEnumerator(str);
            while (characters.MoveNext())
            {
                string character = characters.Current;
                if (Regex.IsMatch(character, @"^[A-Za-z0-9\-._~!$&'()*+,;=:@]$"))
                {
                    pathEncoded.Append(character);
                }
                else
                {
                    byte[] bytes = e.GetBytes(character);
                    foreach (byte b in bytes)
                    {
                        pathEncoded.Append('%');
                        pathEncoded.Append(b.ToString("X2"));
                    }
                }
            }

            return pathEncoded.ToString();
        }

        public string UriEncode(string s, Encoding e, UriComponent c)
        {
            return null;
        }
    }
}

public enum UriComponent
{
    Scheme
}
