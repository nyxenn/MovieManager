using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class MoviePerson
    {
        public MoviePerson()
        {
        }

        public MoviePerson(int personID, string type)
        {
            PersonID = personID;
            Type = type;
        }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        public string Type { get; set; }
    }
}
