using System.IO;

namespace InternetStandards.Ietf.Csv
{
    public class CsvWriter
    {
        private bool _firstValue = true;

        public CsvWriter(TextWriter writer)
        {
            TextWriter = writer;
        }

        public void WriteFields(string[] values, bool forceEscaped = false)
        {
            foreach(var value in values)
                WriteField(value, forceEscaped);
        }

        public void WriteField(string value, bool forceEscaped = false)
        {
            if(!_firstValue)
                TextWriter.Write(',');
            if (forceEscaped || value.Contains("\r\n") || value.IndexOfAny(new[] { '"', ',' }) > -1)
                value = '"' + value.Replace("\"", "\"\"") + '"';
            TextWriter.Write(value);
            _firstValue = false;
        }

        public void WriteCRLF()
        {
            TextWriter.Write("\r\n");
            _firstValue = true;
        }

        public TextWriter TextWriter { get; private set; }
    }
}
