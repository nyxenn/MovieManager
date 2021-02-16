using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Internal.DataAccess;
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

        public async Task<IViewComponentResult> InvokeAsync(string searchTitle = "", string searchType = "")
        {
            if (searchTitle != "" || searchType != "")
            {
                MovieHandler movieHandler = new MovieHandler(_context);
                var movies = await movieHandler.GetLocalMovies(searchTitle, searchType);
                List<LocalMovie> localMovies = 
                    movies
                    .Select(m => new LocalMovie() { Title = m.Title, Type = m.Type, MovieID = m.MovieID, Poster = m.Poster })
                    .ToList();

                return View(localMovies);
            }

            return View(new List<LocalMovie>());
        }
    }
}
