using Microsoft.EntityFrameworkCore;
using MMApi.Enums;
using MMApi.Helpers;
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
    public class MovieHandler
    {
        private readonly MovieContext _context;
        private readonly string _baseUri = "http://www.omdbapi.com";

        public MovieHandler(MovieContext context)
        {
            _context = context;
        }
        
        public async Task<ApiMovie> GetApiMovie(string title, string type)
        {
            ApiMovie movie = null;

            var builder = new UriBuilder(_baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["apikey"] = "83c8b654";
            query["t"] = title;

            if (type != null && Enum.TryParse<SearchType>(type, out SearchType searchType) )
            {
                query["type"] = type;
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    movie = await response.Content.ReadAsAsync<ApiMovie>();
                }
            }
            movie.ActorList = CreateApiListItems(movie.Actors.Split(','));
            movie.WriterList = CreateApiListItems(movie.Writer.Split(','));
            movie.DirectorList = CreateApiListItems(movie.Director.Split(','));
            movie.GenreList = CreateApiListItems(movie.Genre.Split(','));

            return movie;
        }

        public async Task<ApiMovies> GetApiMovies(string title = "", string type = "", int page = 1)
        {
            ApiMovies movies = new ApiMovies();

            var builder = new UriBuilder(_baseUri);
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["apikey"] = "83c8b654";
            query["s"] = title;
            query["page"] = "" + page;

            if (type != null && Enum.TryParse<SearchType>(type, out SearchType searchType))
            {
                query["type"] = type;
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    movies = await response.Content.ReadAsAsync<ApiMovies>();
                }
            }

            return movies;
        }

        public List<ApiListItem> CreateApiListItems(string[] splitString)
        {
            List<ApiListItem> apiListItems = new List<ApiListItem>();

            foreach (var s in splitString)
            {
                ApiListItem a = new ApiListItem(s.Trim());
                apiListItems.Add(a);
            }

            return apiListItems;
        }

        public async Task<List<Movie>> GetLocalMovies(string title, string type)
        {
            List<Movie> movies = new List<Movie>();

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

        public async Task<Movie> GetExistingLocalMovie(string title, string type)
        {
            Movie movie =
                await _context.Movies
                .FirstOrDefaultAsync(m => m.Title.ToLower().Equals(title.ToLower()) && m.Type == type);

            return movie;
        }

        public async Task<Movie> CreateMovie(string title, string type)
        {
            ApiMovie apiMovie = await GetApiMovie(title, type);
            PersonHelper pHelper = new PersonHelper(_context);
            GenreHelper gHelper = new GenreHelper(_context);

            Movie movie = new Movie(title, type, apiMovie.Poster);
            
            List<MovieGenre> genres = await gHelper.GetMovieGenres(apiMovie);
            List<MoviePerson> actors = await pHelper.GetMoviePersonList(apiMovie.Actors, "actor");
            List<MoviePerson> writer = await pHelper.GetMoviePersonList(apiMovie.Writer, "writer");
            List<MoviePerson> director = await pHelper.GetMoviePersonList(apiMovie.Director, "director");
            List<MoviePerson> moviePersonnel = actors.Concat(writer).Concat(director).ToList();

            movie.People = moviePersonnel;
            movie.Genres = genres;
            movie.imdbID = apiMovie.imdbID; 

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
