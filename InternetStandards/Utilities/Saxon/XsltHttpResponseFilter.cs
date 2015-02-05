using System;
using System.Collections.Generic;
using System.Text;
using InternetStandards.Web;
using Saxon.Api;
using System.Xml;
using System.IO;
using java.util;

namespace InternetStandards.Utilities.Saxon
{
    public class XsltHttpResponseFilter : BaseFilter
    {
        protected DocumentBuilder documentBuilder = null;
        protected Serializer serializer = new Serializer();
        protected XsltTransformer transformer = null;
        
        private MemoryStream outputCapture = new MemoryStream();

        public XsltHttpResponseFilter(Stream baseFilter, DocumentBuilder documentBuilder, XsltTransformer transformer):base(baseFilter)
        {
            this.documentBuilder = documentBuilder;
            this.transformer = transformer;
        }
        
        public override void Write(byte[] buffer, int offset, int count)
        {
            outputCapture.Write(buffer, offset, count);
        }

        public override void WriteByte(byte value)
        {
            outputCapture.WriteByte(value);
        }

        public Properties OutputProperties
        {
            get
            {
                return GetOutputProperties();
            }
        }

        public void SetOutputProperty(QName name, string value)
        {
            serializer.SetOutputProperty(name, value);
        }

        public Properties GetOutputProperties()
        {
            return serializer.GetOutputProperties();
        }

        public override void Close()
        {
            outputCapture.Position = 0;
            serializer.SetOutputStream(BaseStream);
            lock (transformer)
            {
                transformer.InitialContextNode = documentBuilder.Build(outputCapture);
                transformer.Run(serializer);
            }

            serializer.Close(); 
            base.Close();
        }
    }
}
