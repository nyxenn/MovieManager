using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMApi.Models
{
    public class DbMovie
    {
        [Key]
        public int MovieID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Poster { get; set; }

        public int DirectorID { get; set; }


        public Person Director { get; set; }

        public Person Writer { get; set; }

        public ICollection<MoviePerson> Actors { get; set; }

        public ICollection<MovieGenre> Genres { get; set; }

        public ICollection<ListMovie> Lists { get; set; }
    }
}
