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
            searchVM.Type = "movie";
            searchVM.Page = 1;
            searchVM.Person = "";
            searchVM.Genre = "";
        }

        private SearchSource PopulateViewModel(string title = "", string type = "", string source = "local", string genre = "", string person = "", int page = 1)
        {
            SearchSource result = SearchSource.local;

            if (title != "")
            {
                searchVM.Title = title;
            }

            if (type != "")
            {
                searchVM.Type = type;
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

            if (source != "" && Enum.TryParse<SearchSource>(source, out SearchSource searchSource))
            {
                result = searchSource;
                searchVM.Source = source;
            }
            else
            {
                ResetViewModel();
            }

            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            searchVM.UserID = userID;

            return result;
        }

        public async Task<IActionResult> Index(string title = "", string type = "", string source = "", string genre = "", string person = "", int page = 1)
        {
            try
            {
                SearchSource searchSource = PopulateViewModel(title, type, source, genre, person, page);

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

        //public async Task<IActionResult> Index(string? title, string? type, string? source)
        //{
        //    searchVM.Title = title != null ? title : "";
        //    searchVM.Type = type != null ? type : "";

        //    if (source == null || Enum.TryParse<SearchSource>(source, out SearchSource searchSource) == false)
        //    {
        //        searchVM.Movie = null;
        //        searchVM.Movies = new List<Movie>();
        //        searchVM.Source = null;
        //        searchVM.Title = null;
        //        searchVM.Type = null;
        //    }
        //    else if (title != null && title != "")
        //    {
        //        if (searchSource == SearchSource.omdb)
        //        {
        //            MovieHandler movieHandler = new MovieHandler(_context);
        //            searchVM.Movie = await movieHandler.GetApiMovie(title, type);
        //        }
        //    }

        //    string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    searchVM.UserID = userID;

        //    var lists = await _context.UserLists.Where(ul => ul.UserID == userID).ToListAsync();
        //    _userLists.AddItemsToList(lists);

        //    return View(searchVM);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Title", "Type", "Source", "Person", "Genre", "Page")] MovieSearchViewModel data)
        {
            PopulateViewModel(data.Title, data.Type, data.Source, data.Genre, data.Person, data.Page);

            return RedirectToAction("Index", new
            {
                title = searchVM.Title,
                type = searchVM.Type,
                source = searchVM.Source,
                person = searchVM.Person,
                genre = searchVM.Genre,
                page = searchVM.Page
            });

            //if (formData.Source == "omdb")
            //{
            //    searchVM.Source = formData.Source;

            //    MovieHandler processor = new MovieHandler(_context);
            //    searchVM.Movie = await processor.GetApiMovie(formData.Title, formData.Type);

            //    return RedirectToAction("Index", new { title = formData.Title, type = formData.Type, source = formData.Source });
            //}
            //else if (formData.Source == "local")
            //{
            //    return RedirectToAction("Index", new { title = formData.Title, type = formData.Type, source = formData.Source });
            //}
            
            //return RedirectToAction("Index");
        }
    }
}
