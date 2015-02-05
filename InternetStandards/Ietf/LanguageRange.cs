using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public abstract class LanguageRange
    {
        string languageRange;
        public LanguageRange(string languageRange)
        {
            this.languageRange = languageRange;
            Subtags = languageRange.Split(new char[] { '-' });
        }

        public virtual string[] Subtags
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return languageRange;
        }
    }
}
