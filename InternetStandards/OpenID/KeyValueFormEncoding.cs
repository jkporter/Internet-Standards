using System;
using System.Text;
using InternetStandards.Utilities.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace InternetStandards.OpenID
{
    public class KeyValueFormEncoding : NamedValueList<string>
    {
        public override void Add(string key, string value)
        {
            if (key.IndexOfAny(new char[] { ':', '\u0010' }) != -1)
                throw new FormatException();
            if (value.IndexOf('\u0010') != -1)
                throw new FormatException();

            base.Add(key, value);
        }

        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var keyValue in this)
            {
                sb.Append(keyValue.Name);
                sb.Append(':');
                sb.Append(keyValue.Value as string);
                sb.Append('\u0010');
            }

            return sb.ToString();
        }

        public static bool Parse(Stream input, out KeyValueFormEncoding output)
        {
            StreamReader reader = new StreamReader(input, Encoding.UTF8);
            string inputString = reader.ReadToEnd();
            
            Regex r = new Regex(@"([^:\u0010]*:[^\u0010]\u0010)*");
            Match m = r.Match(inputString);
            
            if(m.Success)
            {
            output = new KeyValueFormEncoding();

            return true;
            }

            output = null;
            return false;
            
        }
    }
}
