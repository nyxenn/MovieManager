using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.ViewModels
{
    public class MovieSearchViewModel
    {
        public List<ApiMovieShort> ApiMovies { get; set; } = new List<ApiMovieShort>();

        public List<Movie> Movies { get; set; } = new List<Movie>();

        public string? Title { get; set; }

        public string Type { get; set; } = "movie";

        public string Source { get; set; } = "local";

        public string? UserID { get; set; } = "";

        public int Page { get; set; } = 1;

        public string? Person { get; set; } = "";

        public string? Genre { get; set; } = "";

        public bool IsMovie { get; set; }
        public bool IsSeries { get; set; }

    }
}
