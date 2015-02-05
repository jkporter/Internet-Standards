using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class BasicLanguageRange:LanguageRange
    {
        private string languageRange;
        public BasicLanguageRange(string languageRange):base(languageRange)
        {
            //const string basicLanguageRangePattern = @"(?<languageRange>([a-zA-Z]{1,8}(?:-[a-zA-Z\d]{1,8})*)|\*)";
        }
    }
}
