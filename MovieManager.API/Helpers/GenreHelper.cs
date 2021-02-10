using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMApi.Helpers
{
    public class GenreHelper
    {
        private readonly MovieContext _context;

        public GenreHelper(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenres(ApiMovie apiMovie)
        {
            string[] genresSplit = apiMovie.Genre.Split(',');
            List<Genre> genres = new List<Genre>();
            int newGenresCount = 0;

            foreach (var g in genresSplit)
            {
                Genre genre = _context.Genres.FirstOrDefault(gr => gr.Name == g.Trim());

                if (genre == null)
                {
                    genre = new Genre(g.Trim());
                    _context.Genres.Add(genre);
                    newGenresCount++;
                }

                genres.Add(genre);
            }

            if (newGenresCount > 0)
            {
                await _context.SaveChangesAsync();
            }

            return genres;
        }

        public async Task<List<MovieGenre>> GetMovieGenres(ApiMovie apiMovie)
        {
            List<MovieGenre> movieGenres = new List<MovieGenre>();
            List<Genre> genres = await GetGenres(apiMovie);

            foreach (var genre in genres)
            {
                MovieGenre movieGenre = new MovieGenre();
                movieGenre.Genre = genre;
                //movieGenre.GenreID = genre.GenreID;
                movieGenres.Add(movieGenre);
            }

            return movieGenres;
        }
    }
}
