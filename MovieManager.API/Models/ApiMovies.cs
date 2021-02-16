using System;
using System.Collections.Generic;
using System.Text;

namespace MMApi.Models
{
    public class ApiMovies
    {
        public List<ApiMovieShort> Search { get; set; }
        public int totalResults { get; set; }
        public bool Response { get; set; }
    }
}