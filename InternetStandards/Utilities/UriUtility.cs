using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Utilities
{
    public class UriUtility
    {
        public static char[] Reserved = { ':', '/', '?', '#', '[', ']', '@', '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '=' };
        public static char[] Unreserved = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-._~".ToCharArray();

        public static string FilePathToFileUrl(string path, bool useLocalhost = false)
        {
            var host = useLocalhost ? "localhost" : string.Empty;
            string urlPath; 

            if (path.StartsWith(@"\\?\UNC\", StringComparison.CurrentCultureIgnoreCase))
            {
                var urlPathDelimiter = path.IndexOf('\\', 8);
                host = path.Substring(8, urlPathDelimiter - 8);
                urlPath = path.Substring(urlPathDelimiter);
            }
            else if (path.StartsWith(@"\\"))
            {
                var urlPathDelimiter = path.IndexOf('\\', 2);
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

            var sb = new StringBuilder("file://");
            sb.Append(host);
            foreach (var segment in urlPath.Split(pathDelimiters))
            {
                sb.Append('/');
                sb.Append(PathSegmentEncode(segment));
            }

            return sb.ToString();
        }

        public static string Encode(string value, Encoding encoding)
        {
            var uriEncoding = new StringBuilder();
            var characters = new CharacterEnumerator(value);
            while (characters.MoveNext())
            {
                if (characters.Current.Length == 1 && Unreserved.Contains(characters.Current[0]))
                {
                    uriEncoding.Append(characters.Current);
                }
                else
                {
                    foreach (var b in encoding.GetBytes(characters.Current))
                    {
                        uriEncoding.Append('%');
                        uriEncoding.Append(b.ToString("X2"));
                    }
                }
            }

            return uriEncoding.ToString();
        }

        public static string PathSegmentEncode(string str)
        {
            return PathSegmentEncode(str, Encoding.UTF8);
        }

        public static string PathSegmentEncode(string str, Encoding e)
        {
            var pathEncoded = new StringBuilder();
            var characters = new CharacterEnumerator(str);
            while (characters.MoveNext())
            {
                var character = characters.Current;
                if (Regex.IsMatch(character, @"^[A-Za-z0-9\-._~!$&'()*+,;=:@]$"))
                {
                    pathEncoded.Append(character);
                }
                else
                {
                    var bytes = e.GetBytes(character);
                    foreach (var b in bytes)
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
