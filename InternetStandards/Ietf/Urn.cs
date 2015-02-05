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
            Parse();
        }

        private string namespaceIdentifier = null;
        public string NamespaceIdentifier
        {
            get
            {
                return namespaceIdentifier;
            }
        }

        private string namespaceSpecificString = null;
        public string NamespaceSpecificString
        {
            get
            {
                return namespaceSpecificString;
            }
        }

        private void Parse()
        {
            Regex urnRegEx = new Regex(@"(?i:urn):(?<nid>[A-Za-z0-9]([A-Za-z0-9\-]{1,31})?):(?<nss>([A_Za-z0-9()+,\-.:=@;$_!*'%/?#]|%[A-Fa-f0-9]{2})+)");
        }

        public override Uri Normalize()
        {
            return (Uri)new Urn(Scheme.ToLowerInvariant() + ':' + NamespaceIdentifier.ToLowerInvariant() + ':' + this.NamespaceSpecificString);
        }
    }
}
