using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMApi.DataAccess;
using MMApi.Helpers;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using MovieManager.Areas.Content.ViewModels;
using MovieManager.Data;
using MovieManager.Models;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    [Authorize]
    public class ListController : Controller
    {
        private readonly MovieContext _context;

        public ListController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            UserListsViewModel listsViewModel = new UserListsViewModel();

            var lists =
                _context.UserLists
                .Where(ul => ul.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier));

            //var listMovies =
            //    _context.ListMovies
            //    .Where(lm => lm.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier))
            //    .ToList();

            //UserList defaultList = null;
            //List<int> addedListIds = new List<int>();

            //foreach (var lm in listMovies)
            //{
            //    UserList userList = _context.UserLists.FirstOrDefault(ul => ul.UserListID == lm.UserListID);

            //    if (lm.IsDefault)
            //    {
            //        defaultList = userList;
            //    }

            //    addedListIds.Add(userList.UserListID);
            //}

            listsViewModel.Lists = lists.ToList();

            return View(listsViewModel);
        }

        public IActionResult Overview(int id)
        {
            ListOverviewViewModel overviewVM = new ListOverviewViewModel();

            //var list =
            //    _context.UserLists
            //    .Include(ul => ul.Movies)
            //        .ThenInclude(ulm => ulm.Movie)
            //    .FirstOrDefault(ul => ul.UserListID == id);

            //overviewVM.UserList = list;

            return View(overviewVM);
        }

        public void Delete(int id)
        {
            return;
        }

        [HttpPost]
        public async Task<bool> AddToList(int listID, string title, string type, bool isDefault = false)
        {
            try
            {
                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                UserListHandler userListHandler = new UserListHandler(_context);

                if (isDefault)
                {
                    await userListHandler.AddToDefaultList(listID, title, type, userID);
                }
                else
                {
                    userListHandler.AddToList(listID);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
