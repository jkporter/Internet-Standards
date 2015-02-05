using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.ComponentModel;
using System.Web;

namespace InternetStandards.Web
{
    public class HtmlGenericEmptyControl : HtmlControl
    {
        // Methods
        protected HtmlGenericEmptyControl()
            : this("span")
        {
        }

        public HtmlGenericEmptyControl(string tag)
            : base(tag)
        {
        }
    }
}