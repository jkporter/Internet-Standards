using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class Parameter
    {
        private string internalAttribute;
        private string internalValue;

        internal Parameter(string attribute, string value)
        {
            internalAttribute = attribute;
            internalValue = value;
        }

        public string Attribute
        {
            get
            {
                return internalAttribute;
            }
        }

        public string Value
        {
            get
            {
                return internalValue;
            }
        }
    }
}
