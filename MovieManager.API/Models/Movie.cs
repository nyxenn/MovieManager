using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMApi.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public string imdbID { get; set; }

        public virtual ICollection<MoviePerson> People { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<ListMovie> Lists { get; set; }

        public Movie()
        {
        }

        public Movie(string title, string type, string posterUrl)
        {
            Title = title;
            Type = type;
            Poster = posterUrl;
        }
    }
}
