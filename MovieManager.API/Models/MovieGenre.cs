using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class MovieGenre
    {
        public int? MovieID { get; set; }
        public virtual DbMovie Movie { get; set; }

        public int? GenreID { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
