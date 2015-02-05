using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace InternetStandards.Web
{
    public class ImageReplacement
    {
        public void d()
        {
            Image img = new Bitmap(3, 3);
            Graphics g = Graphics.FromImage(img);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.DrawString(
        }
    }
}
