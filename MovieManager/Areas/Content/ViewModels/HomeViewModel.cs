using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.ViewModels
{
    public class HomeViewModel
    {
        public List<Movie> LatestAdditions { get; set; }
        public List<Movie> RandomMovies { get; set; }
    }
}
