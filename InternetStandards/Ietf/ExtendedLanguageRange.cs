using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class ExtendedLanguageRange : LanguageRange
    {
        public ExtendedLanguageRange(string languageRange):base(languageRange)
        {
            // const string extendedLanguageRangePattern = @"(?<extendedLanguageRange>(?:[a-zA-Z]{1,8}|\*)(?:-(?:[a-zA-Z\d]{1,8}|\*))*)";
        }

        public BasicLanguageRange ToBasicLanguageRange()
        {
            if (Subtags[0] == "*")
                return new BasicLanguageRange("*");

            List<string> basicLanguageRangeSubtags = new List<string>();
            foreach (string subtag in this.Subtags)
                if (subtag != "*")
                    basicLanguageRangeSubtags.Add(subtag);

            return new BasicLanguageRange(string.Join("-", basicLanguageRangeSubtags.ToArray()));
        }
    }
}
