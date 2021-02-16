using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Internal.DataAccess;
using MovieManager.Areas.Content.ViewModels;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    public class HomeController : Controller
    {
        private readonly MovieContext _context;
        private MovieHandler _movieHandler;

        public HomeController(MovieContext context)
        {
            _context = context;
            _movieHandler = new MovieHandler(_context);
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeVM = new HomeViewModel();
            homeVM.LatestAdditions = await _movieHandler.GetLatestAdditions(5);
            homeVM.RandomMovies = await _movieHandler.GetRandomMovies(5);

            return View(homeVM);
        }
    }
}
