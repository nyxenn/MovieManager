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
using MovieManager.Areas.Content.Models;
using MovieManager.Areas.Content.ViewModels;

namespace MovieManager.Areas.Content.Controllers
{
    [Area("Content")]
    public class ListController : Controller
    {
        private readonly MovieContext _context;
        private readonly IUserLists _userLists;
        private readonly UserListHandler _listHandler;
        private readonly MovieHandler _movieHandler;

        public ListController(MovieContext context, IUserLists userLists)
        {
            _context = context;
            _userLists = userLists;
            _listHandler = new UserListHandler(_context);
            _movieHandler = new MovieHandler(_context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create([FromBody] CreateList list)
        {
            var userList = await _listHandler.Create(User.FindFirstValue(ClaimTypes.NameIdentifier), list.Title);

            return Json(userList);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            if (id < 0)
            {
                return Json(-1);
            }

            var deletedListID = await _listHandler.Delete(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Json(deletedListID);
        }

        public async Task<JsonResult> GetLists()
        {
            var userLists = await _listHandler.GetListsForUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _userLists.AddItemsToList(userLists);

            return Json(userLists);
        }

        public IActionResult Overview(int id)
        {
            ListOverviewViewModel overviewVM = new ListOverviewViewModel();

            UserList list = _listHandler.GetListOverview(listID: id);

            overviewVM.UserList = list;

            return View(overviewVM);
        }

        [HttpPost]
        public async Task<JsonResult> AddToList([FromBody] AddRemoveList requestDetails)
        {
            return Json(await HandleAddRemoveRequest(requestDetails, "add"));
        }

        public async Task<JsonResult> RemoveFromList([FromBody] AddRemoveList requestDetails)
        {
            return Json(await HandleAddRemoveRequest(requestDetails, "remove"));
        }

        public async Task<int> HandleAddRemoveRequest (AddRemoveList requestDetails, string action)
        {
            try
            {
                if (requestDetails.ListID <= 0 || requestDetails.Title == null || requestDetails.Type == null)
                {
                    throw new ArgumentNullException();
                }

                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (action == "remove")
                {
                    await _listHandler.RemoveFromList(requestDetails.ListID, requestDetails.Title, requestDetails.Type);
                }
                else if (requestDetails.IsDefault)
                {
                    await _listHandler.AddToDefaultList(requestDetails.ListID, requestDetails.Title, requestDetails.Type, userID);
                }
                else
                {
                    await _listHandler.AddCustomToList(requestDetails.ListID, requestDetails.Title, requestDetails.Type);
                }

                return requestDetails.ListID;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUserLists(string title, string type)
        {
            Movie movie = await _movieHandler.GetExistingLocalMovie(title, type);
            List<UserList> lists = _userLists.GetLists();
            var listsWithMovieAdded = await _listHandler.GetListsWithMovieAdded(lists, movie);

            return Json(listsWithMovieAdded);
        }
    }
}
