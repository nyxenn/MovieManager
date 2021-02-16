using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using MovieManager.Areas.Content.Models;
using MovieManager.Areas.Content.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Content.Components
{
    [ViewComponent(Name = "LocalMovies")]
    public class LocalMoviesComponent : ViewComponent
    {
        private readonly MovieContext _context;

        public LocalMoviesComponent(MovieContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchTitle = "", string searchType = "", string person = "", string genre = "", int page = 1)
        {
            MovieHandler movieHandler = new MovieHandler(_context);
            List<LocalMovie> localMovies = new List<LocalMovie>();
            List<Movie> movies = new List<Movie>();

            if (genre == "" && person == "" && searchTitle == "" && searchType == "")
            {
                return View(new List<LocalMovie>());
            }


            if (genre != "")
            {
                movies = await movieHandler.GetLocalMoviesByGenre(genre, page);
            }
            else if (person != "")
            {
                movies = await movieHandler.GetLocalMoviesByPerson(person, page);
            }
            else
            {
                movies = await movieHandler.GetLocalMovies(searchTitle, searchType, page);
            }

            localMovies =
                    movies
                    .Select(m => new LocalMovie() { Title = m.Title, Type = m.Type, MovieID = m.MovieID, Poster = m.Poster })
                    .ToList();

            return View(localMovies);
        }
    }
}
