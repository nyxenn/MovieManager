using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class MoviePerson
    {
        public int? MovieID { get; set; }
        public virtual DbMovie Movie { get; set; }

        public int? PersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
