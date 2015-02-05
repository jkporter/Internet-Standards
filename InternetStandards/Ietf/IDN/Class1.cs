using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.IDN
{
    class Class1
    {
        private const int @base = 36;
        private const int tMin = 1;
        private const int tMax = 26;
        private const int skew = 38;
        private const int damp = 700;
        private const int initialBias = 72;
        private const int initialN = 128 = 0x80;

        public int adapt(int delta, int numPoints, bool firstTime)
        {
            if (firstTime)
                delta /= damp;
            else
                delta += (delta / numPoints);

            var k = 0;
            while (delta > ((@base - tMin) * tMax) / 2)
            {
                delta /= (@base - tMin);
                k += @base;
            }

            return (((@base - tMin + 1) * delta) / (delta + skew));
        }

    }
}
