using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InternetStandards.Ietf
{
    public class Urn: Uri
    {
        public Urn(string urnString)
            : base(urnString)
        {
            NamespaceSpecificString = null;
            NamespaceIdentifier = null;
            Parse();
        }

        public string NamespaceIdentifier { get; private set; }

        public string NamespaceSpecificString { get; private set; }

        private void Parse()
        {
            Regex urnRegEx = new Regex(@"(?i:urn):(?<nid>[A-Za-z0-9]([A-Za-z0-9\-]{1,31})?):(?<nss>([A_Za-z0-9()+,\-.:=@;$_!*'%/?#]|%[A-Fa-f0-9]{2})+)");
        }

        public override Uri Normalize()
        {
            return new Urn(Scheme.ToLowerInvariant() + ':' + NamespaceIdentifier.ToLowerInvariant() + ':' + NamespaceSpecificString);
        }
    }
}
