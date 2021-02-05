using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.ViewModels
{
    public class MovieSearchViewModel
    {
        public Movie? Movie { get; set; }
        public List<DbMovie> Movies { get; set; } = new List<DbMovie>();
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Source { get; set; }
    }
}
