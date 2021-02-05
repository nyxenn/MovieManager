using Microsoft.EntityFrameworkCore;
using MMApi.Enums;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMApi.DataAccess
{
    public class MovieProcessor
    {
        private readonly MovieContext _context;

        public MovieProcessor(MovieContext context)
        {
            _context = context;
        }
        
        public async Task<Movie> GetApiMovie(string title, string type)
        {
            Movie movie = null;

            var builder = new UriBuilder("http://www.omdbapi.com");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["i"] = "tt3896198";
            query["apikey"] = "83c8b654";
            query["t"] = title;

            if (type != null && Enum.TryParse<SearchType>(type, out SearchType searchType) )
            {
                query["type"] = type;
            }
            else
            {
                query["type"] = "movie";
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    movie = await response.Content.ReadAsAsync<Movie>();
                }
            }

            return movie;
        }

        public async Task<List<DbMovie>> GetLocalMovies(string title, string type)
        {
            List<DbMovie> movies = new List<DbMovie>();

            if (type == null || Enum.TryParse<SearchType>(type, out SearchType searchType) == false)
            {
                type = "movie";
            }

           var dbMovies =
                _context.Movies
                .Where(m => m.Title.ToLower().Contains(title.ToLower()))
                .Where(m => m.Type.ToLower() == type.ToLower());

            movies = await dbMovies.ToListAsync();

            return movies;
        }
    }
}
