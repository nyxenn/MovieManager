using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.ViewModels
{
    public class MovieSearchViewModel
    {
        public ApiMovie? Movie { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public string? Title { get; set; }

        public string? Type { get; set; }

        public string? Source { get; set; }

        public int? DefaultList { get; set; }

        public string? UserID { get; set; }

    }
}
