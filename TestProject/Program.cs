using System;
using System.Collections.Generic;
using System.Text;
using InternetStandards.Ietf;
using InternetStandards.Ietf.vCard;
using InternetStandards.Ietf.vCard.v4;
using InternetStandards.Ietf.xCard;
using InternetStandards.JsonML;
using InternetStandards.Xml;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Globalization;
using InternetStandards.W3c.XForms;
using System.IO;
using System.Linq;
using InternetStandards.Utilities;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;


namespace TestProject
{
    class Program
    {
        public static void Main()
        {

            /* using (var input = new FileStream("Test.vcf", FileMode.Open))
            {
                var reader = new vCardReader(input);
                while (reader.Read())
                {
                    Console.WriteLine(reader.Name);
                    foreach (var p in reader.Parameters)
                    {
                        Console.Write(p.Key + "=");
                        Console.WriteLine(string.Join(", ", p .Value));
                    }
                    Console.WriteLine(reader.Value);
                    Console.WriteLine();
                }
                
            } */



            var w1 = new JsonTextWriter(Console.Out);
            w1.Formatting = Formatting.Indented;
            var w = new JsonMLWriter(w1);
            var d = new XmlDocument();
            d.Load(@"D:\Users\Jonathan\Documents\Visual Studio 2010\Projects\InternetStandards\Web.UI.Xhtml1StrictToHtml401StrictFilter.xslt");

            d.WriteTo(w);

            w.Close();
            w1.Close();

            
            Console.Read();
        }

        public static string Test(Match m)
        {
            if (m.Groups["space"].Success)
                return " ";

            if (m.Groups["lineBreak"].Success)
                return "\u2028";

            if (m.Groups["nonAlphaNumeric"].Success)
                return ASCIIEncoding.ASCII.GetString( new byte[] { Convert.ToByte(m.Groups["nonAlphaNumeric"].Value.Substring(1), 16) } );

            return m.Value;
        }
    }
}
