using System;
using System.Text;
namespace InternetStandards.Ietf.Http
{
    public class HttpAcceptHeaderValue
    {

        public HttpAcceptHeaderValue(string httpAcceptHeaderValueString)
        {
            int num2 = 0;
            this.qualityValue = 1F;
            httpAcceptHeaderValueString = httpAcceptHeaderValueString.Trim();
            int num4 = httpAcceptHeaderValueString.Length;
            int num3 = 0;
            bool flag2 = false;
            bool flag1 = false;
            while ((((num3 < num4) && (httpAcceptHeaderValueString.ToCharArray()[num3] != '/')) && !(char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3]))))
            {
                num3 += 1;
            }
            if (((num3 >= num4) || (httpAcceptHeaderValueString.ToCharArray()[num3] != '/')))
            {
            }
            num3 += 1;
            while ((((num3 < num4) && !(char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3]))) && (httpAcceptHeaderValueString.ToCharArray()[num3] != ';')))
            {
                num3 += 1;
            }
            int num1 = num3;
            while (((num3 < num4) && char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3])))
            {
                num3 += 1;
            }
            if ((num3 < num4))
            {
                if ((httpAcceptHeaderValueString.ToCharArray()[num3] != ';'))
                {
                }
                num3 += 1;
                while ((num3 < num4))
                {
                    while (((num3 < num4) && char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3])))
                    {
                        num3 += 1;
                    }
                    StringBuilder builder1 = new StringBuilder();
                    while ((((num3 < num4) && !(char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3]))) && (httpAcceptHeaderValueString.ToCharArray()[num3] != '=')))
                    {
                        builder1.Append(httpAcceptHeaderValueString.ToCharArray()[num3]);
                        num3 += 1;
                    }
                    string text1 = builder1.ToString();
                    if ((httpAcceptHeaderValueString.ToCharArray()[num3] != '='))
                    {
                    }
                    num3 += 1;
                    if (char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3]))
                    {
                    }
                    bool flag3 = (httpAcceptHeaderValueString.ToCharArray()[num3] == '"');
                    if (flag3)
                    {
                        num3 += 1;
                    }
                    builder1.Length = 0;
                    while ((((num3 < num4) && (flag3 || (!(char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3])) && (httpAcceptHeaderValueString.ToCharArray()[num3] != ';')))) && (httpAcceptHeaderValueString.ToCharArray()[num3] != '"')))
                    {
                        builder1.Append(httpAcceptHeaderValueString.ToCharArray()[num3]);
                        num3 += 1;
                    }
                    if ((flag3 && (httpAcceptHeaderValueString.ToCharArray()[num3] != '"')))
                    {
                    }
                    if (flag3)
                    {
                        num3 += 1;
                    }
                    while (((num3 < num4) && char.IsWhiteSpace(httpAcceptHeaderValueString.ToCharArray()[num3])))
                    {
                        num3 += 1;
                    }
                    if (((num3 < num4) && (httpAcceptHeaderValueString.ToCharArray()[num3] != ';')))
                    {
                    }
                    if (text1.ToLowerInvariant() == "q")
                    {
                        this.qualityValue = float.Parse(builder1.ToString());
                        flag2 = true;
                        flag1 = (num3 < num4);
                        num2 = num3;
                        break;
                    }
                    num1 = num3;
                    num3 += 1;
                }
            }
            if (flag2)
            {
                this._mediaRange = new Http.MediaRange(httpAcceptHeaderValueString.Substring(0, num1));
                if (flag1)
                {
                    this._extension = httpAcceptHeaderValueString.Substring(num2);
                }
            }
            else
            {
                this._mediaRange = new Http.MediaRange(httpAcceptHeaderValueString.Trim());
            }
        }

        public override string ToString()
        {
            return (MediaRange.ToString() + ";q=" + Quality.ToString("0.###") + Extension);
        }

        public string Extension
        {
            get
            {
                return this._extension;
            }
        }

        public Http.MediaRange MediaRange
        {
            get
            {
                return this._mediaRange;
            }
        }

        public float Quality
        {
            get
            {
                return this.qualityValue;
            }
        }
        private string _extension;
        private Http.MediaRange _mediaRange;
        private float qualityValue;
    }
}