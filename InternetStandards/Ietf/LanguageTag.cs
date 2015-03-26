using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class LanguageTag
    {
        private const string LanguageTagRegExPattern = @"(?<languageTag>(?<langTag>(?<language>(?:[a-zA-Z]{2,3}(?:-(?<extLang>[a-zA-Z]{3}(?:-[a-zA-Z]{3}){0,2}))?)|[a-zA-Z]{4}|[a-zA-Z]{5,8})(?:-(?<script>[a-zA-Z]{4}))?(?:-(?<region>(?:[a-zA-Z]{2}|\d{3})))?(?:-(?<variant>(?:[a-zA-Z\d]{5,8}|\d[a-zA-Z\d]{3})))*(?:-(?<extension>(?<singleton>[\d\x41-\x57\x59-\x5A\x61-\x77\x79-\x7A])(?:-[a-zA-Z\d]{2,8})+))*(?:-(?<privateUse>x(?:-[a-zA-Z\d]{1,8})+))?)|(?<privateUse>x(?:-[a-zA-Z\d]{1,8})+)|(?<grandfathered>(?<irregular>en-GB-oed|i-ami|i-bnn|i-default|i-enochian|i-hak|i-klingon|i-lux|i-mingo|i-navajo|i-pwn|i-tao|i-tay|i-tsu|sgn-BE-FR|sgn-BE-NL|sgn-CH-DE)|(?<regular>art-lojban|cel-gaulish|no-bok|no-nyn|zh-guoyu|zh-hakka|zh-min|zh-min-nan|zh-xiang)))";

        private static readonly Regex LanguageTagRegEx;

        static LanguageTag()
        {
            LanguageTagRegEx = new Regex(LanguageTagRegExPattern, RegexOptions.Compiled);
        }

        public LanguageTag(string languageTag)
        {
            var match = LanguageTagRegEx.Match(languageTag);
            if (!match.Success)
                throw new FormatException();

            Subtags = languageTag.Split('-');

            /* extendedLanguageSubtags = GetGroupValueByNameAndSplit(match, "extLang");
            scriptSubtag = GetGroupValueByName(match, "script");
            regionSubtag = GetGroupValueByName(match, "region");
            variantSubtags = GetGroupValueByNameAndSplit(match, "variant");
            //extensions = GetGroupValueByNameAndSplit(match, "extension"); */
            PrivateUseSubtags = GetGroupValueByNameAndSplit(match, "privateUse");

        }

        public string[] Subtags { get; private set; }

        protected string PrimarySubtag
        {
            get
            {
                return Subtags[0];
            }
        }

        public string PrimaryLanguageSubtag
        {
            get
            {
                return (PrimarySubtag.ToLowerInvariant() != "x" && PrimarySubtag.ToLowerInvariant() != "i") ? PrimarySubtag : null;
            }
        }

        public string[] PrivateUseSubtags { get; private set; }

        private static string GetGroupValueByName(Match match, string groupName)
        {
            return match.Groups[groupName].Success ? match.Groups[groupName].Value : null;
        }

        private static string[] GetGroupValueByNameAndSplit(Match match, string groupName)
        {
            var groupValue = GetGroupValueByName(match, groupName);
            return groupValue != null ? groupValue.Split('-') : null;
        }
    }
}
