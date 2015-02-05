namespace InternetStandards.Web.UI.HtmlControls
{
    /// <summary>
    /// Summary description for HtmlImage
    /// </summary>
    public class HtmlImage : System.Web.UI.HtmlControls.HtmlImage
    {
        public HtmlImage()
            : base()
        {
            Attributes.Add("alt", string.Empty);
        }

        public new string Alt
        {
            get
            {
                return Attributes["alt"];
            }
            set
            {
                Attributes.Add("alt", value);
            }
        }
    }
}
