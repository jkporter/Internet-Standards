using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Mail.Mime
{
    public class Parameter
    {
        protected string name = null;
        protected string[] value = null;
        protected Encoding e = null;
        protected string language = null;

        public Parameter(string attribute, string value)
            : this(attribute, value, null, null)
        {
        }

        public Parameter(string name, string value, Encoding e, string language)
            : this(name, new string[] { value }, e, language)
        {
        }

        public Parameter(string name, string[] value, Encoding e, string language)
        {
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Value
        {
            get
            {
                return string.Concat(value);
            }
        }

        public override string ToString()
        {
            return name + '=' + e.WebName + '\'' + language + '\'';
        }
    }
}
