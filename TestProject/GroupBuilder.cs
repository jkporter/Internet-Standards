using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class GroupBuilder:Builder
    {
        string name = null;
        public GroupBuilder(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            if (name != null)
            {
                sb.Append("?<");
                sb.Append(name);
                sb.Append(">");
            }
            sb.Append(base.ToString());
            sb.Append(")");

            return sb.ToString();
        }
    }
}
