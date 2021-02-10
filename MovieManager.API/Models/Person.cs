using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMApi.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MoviePerson> Movies { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }
}
