using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class Builder
    {
        private List<object> objects = new List<object>();
        public object a(object o)
        {
            objects.Add(o);
            return o;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in objects)
                sb.Append(o.ToString());
            return sb.ToString();
        }
    }
}
