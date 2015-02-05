using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf
{
    public class SimpleLanguagePriorityList:List<BasicLanguageRange>
    {
        public LanguageTag[] Filter(LanguageTag[] languageTags)
        {
            List<LanguageTag> filteredLanguageTags = new List<LanguageTag>(Count);
            foreach (var value in this)
            {
                LanguageTag matchingLanguageTag = null;
                int? matchingLanguageTagSepcificy = null;
                foreach (var languageTag in languageTags)
                {
                    if (!filteredLanguageTags.Contains(languageTag)
                        && Match(value, languageTag)
                        && (matchingLanguageTagSepcificy == null || languageTag.Subtags.Length < matchingLanguageTagSepcificy))
                    {
                        matchingLanguageTagSepcificy = languageTag.Subtags.Length;
                        matchingLanguageTag = languageTag;
                    }
                }

                if (matchingLanguageTag != null)
                    filteredLanguageTags.Add(matchingLanguageTag);
            }

            return filteredLanguageTags.ToArray();
        }

        public LanguageTag Lookup(LanguageTag[] languageTags)
        {
            var filteredLanguageTags = new List<LanguageTag>(Count);
            foreach (var value in this)
            {
                for(int i = value.Subtags.GetUpperBound(0); i >= 0; i--)
                {
                    string valueToMatch = string.Join("-", value.Subtags.Take(i).ToArray());
                    foreach (var tag in languageTags)
                        if (string.Compare(valueToMatch, tag.ToString(), true) == 0)
                            return tag;
                }
            }

            return null;
        }

        public static bool Match(LanguageRange languageRange, LanguageTag languageTag)
        {
            if (languageRange.ToString() == "*" || string.Compare(languageRange.ToString(), languageTag.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0)
                return true;

            if (languageRange.Subtags.Length <= languageTag.Subtags.Length)
            {
                for (int i = 0; i < languageRange.Subtags.Length; i++)
                    if (string.Compare(languageRange.Subtags[i], languageTag.Subtags[i], StringComparison.InvariantCultureIgnoreCase) != 0)
                        return false;
                return true;
            }

            return false;
        }
    }
}
