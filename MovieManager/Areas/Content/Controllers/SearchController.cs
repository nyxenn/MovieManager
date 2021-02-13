using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Enums;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using MovieManager.Areas.Content.Models;
using MovieManager.Areas.Content.ViewModels;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    public class SearchController : Controller
    {
        private readonly MovieContext _context;
        private readonly IUserLists _userLists;
        public static MovieSearchViewModel searchVM = new MovieSearchViewModel();

        public SearchController(MovieContext context, IUserLists userLists)
        {
            _context = context;
            _userLists = userLists;
        }

        public async Task<IActionResult> Index(string? title, string? type, string? source)
        {
            searchVM.Title = title != null ? title : "";
            searchVM.Type = type != null ? type : "";

            if (source == null || Enum.TryParse<SearchSource>(source, out SearchSource searchSource) == false)
            {
                searchVM.Movie = null;
                searchVM.Movies = new List<Movie>();
                searchVM.Source = null;
                searchVM.Title = null;
                searchVM.Type = null;
            }
            else if (title != null && title != "")
            {
                if (searchSource == SearchSource.omdb)
                {
                    MovieHandler movieHandler = new MovieHandler(_context);
                    searchVM.Movie = await movieHandler.GetApiMovie(title, type);
                }
                else
                {
                    MovieHandler movieHandler = new MovieHandler(_context);
                    searchVM.Movies = await movieHandler.GetLocalMovies(title, type);
                }
            }

            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            searchVM.UserID = userID;

            var lists = await _context.UserLists.Where(ul => ul.UserID == userID).ToListAsync();
            _userLists.AddItemsToList(lists);

            return View(searchVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Title", "Type", "Source")] MovieSearchViewModel formData)
        {
            if (formData.Source == "omdb")
            {
                searchVM.Source = formData.Source;

                MovieHandler processor = new MovieHandler(_context);
                searchVM.Movie = await processor.GetApiMovie(formData.Title, formData.Type);

                return RedirectToAction("Index", new { source = formData.Source });
            }
            else if (formData.Source == "local")
            {
                searchVM.Source = formData.Source;

                MovieHandler processor = new MovieHandler(_context);
                searchVM.Movies = await processor.GetLocalMovies(formData.Title, formData.Type);

                return RedirectToAction("Index", new { source = formData.Source });
            }
            
            return RedirectToAction("Index");
        }
    }
}
