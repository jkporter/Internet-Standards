using System;
using System.Collections.Generic;
using System.Text;
using InternetStandards.Web;
using System.Xml.Xsl;
using System.IO;
using System.Xml;

namespace InternetStandards.Xml.Xsl
{
    public class XsltHttpFilter : BaseFilter
    {
        private MemoryStream outputCapture = new MemoryStream();
        
        protected XslCompiledTransform internalXslCompiledTransform = null;
        protected XsltArgumentList internalXsltArgumentList = null;
        protected string internalBaseUri = null;
        protected XmlResolver internalResolver = null;
        protected bool useDefaultResovler = false;
        
        public XsltHttpFilter(Stream baseFilter, string baseUri, XmlResolver resolver, XslCompiledTransform transform, XsltArgumentList argumentList)
            : base(baseFilter)
        {
            internalBaseUri = baseUri;
            internalResolver = resolver;
            internalXslCompiledTransform = transform;
            internalXsltArgumentList = argumentList;
        }

        public XsltHttpFilter(Stream baseFilter, string baseUri, XslCompiledTransform transform, XsltArgumentList argumentList)
            : this(baseFilter, baseUri, null, transform, argumentList)
        {
            useDefaultResovler = true;
        }

        public XsltHttpFilter(Stream baseFilter, string baseUri, XmlResolver resolver, XslCompiledTransform transform)
            : this(baseFilter, baseUri, resolver, transform, null)
        {
        }

        public XsltHttpFilter(Stream baseFilter, string baseUri, XslCompiledTransform transform)
            : this(baseFilter, baseUri, transform, null)
        {
        }

        public XslCompiledTransform XslCompiledTransform
        {
            get
            {
                return internalXslCompiledTransform;
            }
        }

        public XsltArgumentList XsltArgumentList
        {
            get
            {
                return internalXsltArgumentList;
            }
        }

        public XmlResolver XmlResolver
        {
            set
            {
                internalResolver = value;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            outputCapture.Write(buffer, offset, count);
        }

        public void Dispose()
        {
            outputCapture.Position = 0;
            
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = true;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.DtdProcessing = DtdProcessing.Parse;
            if (!useDefaultResovler)
                settings.XmlResolver = internalResolver;
            
            using (XmlReader input = XmlReader.Create(outputCapture, settings, internalBaseUri))
            {
                internalXslCompiledTransform.Transform(input, XsltArgumentList, BaseStream);
            }
            
            base.Dispose();
        }
    }
}
