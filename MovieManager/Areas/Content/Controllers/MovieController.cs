using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMApi.DataAccess;
using MMApi.Internal.DataAccess;
using MovieManager.Areas.Content.Models;
using Newtonsoft.Json;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    [Authorize]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        private readonly MovieHandler _movieHandler;

        public MovieController(MovieContext context)
        {
            _context = context;
            _movieHandler = new MovieHandler(_context);
        }

        public async Task<IActionResult> Details(string title, string type)
        {
            ApiMovie movie = new ApiMovie();
            movie =
                JsonConvert.DeserializeObject<ApiMovie>
                (JsonConvert.SerializeObject(await _movieHandler.GetApiMovie(title, type)));

            return View(movie);
        }
    }
}
