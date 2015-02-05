using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class LanguageRangeOld
    {
        private string languageRange;
        public LanguageRangeOld(string languageRange)
        {
            const string basicLanguageRangePattern = @"(?<languageRange>([a-zA-Z]{1,8}(?:-[a-zA-Z\d]{1,8})*)|\*)";
            const string extendedLanguageRangePattern = @"(?<extendedLanguageRange>(?:[a-zA-Z]{1,8}|\*)(?:-(?:[a-zA-Z\d]{1,8}|\*))*)";

            this.languageRange = languageRange;
            subtags = languageRange.Split(new char[] { '-' });
        }

        private LanguageRangeType type;
        public LanguageRangeType Type
        {
            get
            {
                return type;
            }
        }

        

        public static LanguageTag Lookup(SortedList<object, BasicLanguageRange> languagePriorityList, List<LanguageTag> languageTags)
        {
            return null;
        }

        public bool Match(LanguageTag languageTag)
        {
            switch (Type)
            {
                case LanguageRangeType.Extended:
                    return ExtendedFilteringMatch(languageTag);
                case LanguageRangeType.Basic:
                default:
                    return BasicFilteringMatch(languageTag);
            }
        }

        public bool BasicFilteringMatch(LanguageTag languageTag)
        {
            if (this.ToString() == "*" || string.Compare(this.ToString(), languageTag.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0)
                return true;

            if (this.Subtags.Length <= languageTag.Subtags.Length)
            {
                for (int i = 0; i < this.Subtags.Length; i++)
                    if (string.Compare(this.Subtags[i], languageTag.Subtags[i], StringComparison.InvariantCultureIgnoreCase) != 0)
                        return false;
                return true;
            }

            return false;
        }

        public bool ExtendedFilteringMatch(LanguageTag languageTag)
        {
            if (SubtagMatch(Subtags[0], languageTag.Subtags[0]))
            {
                int currentlanguageRangeSubTag = 1;
                int currentlanguageTagSubTag = 1;

                while (Subtags.GetUpperBound(0) <= currentlanguageRangeSubTag)
                    if (Subtags[currentlanguageRangeSubTag] == "*")
                    {
                        currentlanguageRangeSubTag++;
                    }
                    else if (languageTag.Subtags.GetUpperBound(0) < currentlanguageTagSubTag)
                    {
                        return false;
                    }
                    else if (SubtagMatch(Subtags[currentlanguageRangeSubTag], languageTag.Subtags[currentlanguageTagSubTag]))
                    {
                        currentlanguageRangeSubTag++;
                        currentlanguageTagSubTag++;
                    }
                    else if (Regex.IsMatch(languageTag.Subtags[currentlanguageTagSubTag], @"^[\dA-Za-z]$"))
                    {
                        return false;
                    }
                    else
                    {
                        currentlanguageTagSubTag++;
                    }

                return true;
            }

            return false;
        }

        private static bool SubtagMatch(string extendedLanguageRangeSubtag, string languageTagSubtag)
        {
            return extendedLanguageRangeSubtag == "*" || string.Compare(extendedLanguageRangeSubtag, languageTagSubtag, StringComparison.InvariantCultureIgnoreCase) == 0;
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

        private string[] subtags;
        public string[] Subtags
        {
            get
            {
                return subtags;
            }
        }

        public override string ToString()
        {
            return languageRange;
        }
    }

    public enum LanguageRangeType
    {
        Basic,
        Extended
    }
}
