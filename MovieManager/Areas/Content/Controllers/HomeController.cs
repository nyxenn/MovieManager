using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.Internal.DataAccess;
using MovieManager.Areas.Content.ViewModels;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            MovieDetailsViewModel detailsVM = new MovieDetailsViewModel();
            detailsVM.Movie =
                _context.Movies
                .Include(m => m.People)
                    .ThenInclude(p => p.Person)
                .FirstOrDefault();

            return View(detailsVM);
        }
    }
}
