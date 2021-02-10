using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMApi.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MovieGenre> Movies { get; set; }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
