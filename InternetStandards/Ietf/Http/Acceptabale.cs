using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetStandards.Ietf.Http
{
    public class Acceptabale
    {
        public Acceptabale(IEnumerable<IQuality> qualityValues)
        {
            var a = from q in qualityValues where q.Quality > 0 orderby q.Quality descending select q;
            var na = from q in qualityValues where q.Quality == 0 select q;

            var ba = from q in a where q.Quality == a.Max(q2 => q2.Quality) select q;
        }

        public IQuality Acceptable
        {
            get
            {
                return null;
            }
        }

        public IQuality[] NotAcceptable
        {
            get
            {
                return null;
            }
        }

        public IQuality[] BestAcceptable
        {
            get
            {
                return null;
            }
        }
    }
}
