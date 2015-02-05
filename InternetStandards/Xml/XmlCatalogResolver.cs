using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace InternetStandards.Xml
{
    class XmlCatalogResolver:XmlResolver
    {
        public override System.Net.ICredentials Credentials
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
