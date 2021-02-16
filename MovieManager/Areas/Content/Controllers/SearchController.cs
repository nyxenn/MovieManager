using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class SearchController : Controller
    {
        private readonly int _pageSize = 10;
        private readonly MovieContext _context;
        private readonly IUserLists _userLists;
        public static MovieSearchViewModel searchVM = new MovieSearchViewModel();

        public SearchController(MovieContext context, IUserLists userLists)
        {
            _context = context;
            _userLists = userLists;
        }

        private void ResetViewModel()
        {
            searchVM.ApiMovies = new List<ApiMovieShort>();
            searchVM.Movies = new List<Movie>();
            searchVM.Source = "local";
            searchVM.Title = "";
            searchVM.Type = "any";
            searchVM.Page = 1;
            searchVM.Person = "";
            searchVM.Genre = "";
        }

        private SearchSource PopulateViewModel
            (string title = "",
            string source = "local",
            string genre = "",
            string person = "",
            int page = 1,
            bool isMovie = false,
            bool isSeries = false)
        {
            SearchSource result = SearchSource.local;

            if (source != "" && Enum.TryParse<SearchSource>(source, out SearchSource searchSource))
            {
                result = searchSource;
                searchVM.Source = source;
            }
            else
            {
                ResetViewModel();
            }

            if (title != "")
            {
                searchVM.Title = title;
            }

            if (isMovie)
            {
                searchVM.Type = "movie";
            }

            if (isSeries)
            {
                searchVM.Type = "series";
            }

            if (isMovie && isSeries)
            {
                searchVM.Type = "any";
            }

            if (searchVM.Source != source)
            {
                searchVM.Page = 1;
            }
            else
            {
                searchVM.Page = page;
            }

            if (genre != "")
            {
                searchVM.Genre = genre;
            }

            if (person != "")
            {
                searchVM.Person = person;
            }

            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            searchVM.UserID = userID;
            searchVM.IsMovie = isMovie;
            searchVM.IsSeries = isSeries;

            return result;
        }

        public async Task<IActionResult> Index(string title = "", string type = "", string source = "", string genre = "", string person = "", int page = 1)
        {
            try
            {
                bool isMovie = type == "movie" ? true : type == "any" ? true : false;
                bool isSeries = type == "series" ? true : type == "any" ? true : false;
                SearchSource searchSource = PopulateViewModel(title, source, genre, person, page, isMovie, isSeries);

                if (searchSource == SearchSource.omdb)
                {
                    MovieHandler movieHandler = new MovieHandler(_context);
                    ApiMovies movies = await movieHandler.GetApiMovies(title, type, page);
                    searchVM.ApiMovies = movies.Search;
                }

                var lists = await _context.UserLists.Where(ul => ul.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
                _userLists.AddItemsToList(lists);

                return View(searchVM);

            }
            catch (Exception)
            { 
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Title", "Source", "Person", "Genre", "Page", "IsMovie", "IsSeries")] MovieSearchViewModel data)
        {
            PopulateViewModel(data.Title, data.Source, data.Genre, data.Person, data.Page, data.IsMovie, data.IsSeries);

            return RedirectToAction("Index", new
            {
                title = searchVM.Title,
                type = searchVM.Type,
                source = searchVM.Source,
                person = searchVM.Person,
                genre = searchVM.Genre,
                page = searchVM.Page
            });
        }
    }
}
