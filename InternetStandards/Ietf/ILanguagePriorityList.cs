using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf
{
    interface ILanguagePriorityList<T>
    {
        LanguageTag[] Filter(LanguageTag[] languageTags);
    }
}
