using System;
using System.Collections.Generic;
using System.Text;

namespace InternetStandards.Ecma.Xmpp
{
    public class JabberIdentifier
    {
        public JabberIdentifier(string jid)
        {
        }

        protected string node = null;
        public string Node
        {
            get
            {
                return node;
            }
        }

        protected string domain = null;
        public string Domain
        {
            get
            {
                return domain;
            }
        }

        protected string resource = null;
        public string Resource
        {
            get
            {
                return resource;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(node);
                sb.Append('@');
            }
            sb.Append(domain);
            if (resource != null)
            {
                sb.Append('/');
                sb.Append(resource);
            }
            return sb.ToString();
        }
    }
}
