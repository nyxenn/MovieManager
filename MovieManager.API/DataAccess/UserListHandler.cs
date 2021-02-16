using Microsoft.EntityFrameworkCore;
using MMApi.Helpers;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MMApi.DataAccess
{
    public class UserListHandler
    {
        private readonly MovieContext _context;
        private readonly MovieHandler _movieHandler;
        private readonly ListMovieHelper _listMovieHelper;
        private readonly MovieHelper _movieHelper;

        public UserListHandler(MovieContext context)
        {
            _context = context;
            _movieHandler = new MovieHandler(_context);
            _listMovieHelper = new ListMovieHelper(_context);
            _movieHelper = new MovieHelper(_context);
        }

        public async Task<UserList> Create(string userID, string title)
        {
            UserList userList = new UserList(userID, title);

            _context.UserLists.Add(userList);
            await _context.SaveChangesAsync();

            return userList;
        }

        public async Task<int> Delete(int listID, string userID)
        {
            int id = -1;
            UserList list = await _context.UserLists.FirstOrDefaultAsync(ul => ul.UserListID == listID && ul.UserID == userID);

            if (list == null)
            {
                return id;
            }

            id = list.UserListID;
            _context.UserLists.Remove(list);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<List<UserList>> GetListsForUser(string userID)
        {
            List<UserList> lists =
                await _context.UserLists
                .Where(ul => ul.UserID == userID)
                .ToListAsync();

            return lists;
        }

        public async Task<List<UserListAdded>> GetListsWithMovieAdded(List<UserList> lists, Movie movie)
        {
            List<UserListAdded> listsWithMovieAdded = new List<UserListAdded>();

            List<int> listIds = lists.Select(l => l.UserListID).ToList();
            List<int> addedLists = new List<int>();

            if (movie != null)
            {
                addedLists =
                await _context.ListMovies
                .Where(lm =>
                    listIds.Contains(lm.UserListID)
                    && lm.MovieID == movie.MovieID)
                .Select(lm => lm.UserListID)
                .ToListAsync();
            }
            

            foreach (var list in lists)
            {
                UserListAdded listAdded = new UserListAdded();
                listAdded.UserList = list;
                if (addedLists.IndexOf(list.UserListID) != -1)
                {
                    listAdded.MovieAdded = true;
                }
                listsWithMovieAdded.Add(listAdded);
            }

            return listsWithMovieAdded;
        }

        public UserList GetListOverview(int listID)
        {
            UserList list =
                _context.UserLists
                .Include(ul => ul.Movies)
                    .ThenInclude(ulm => ulm.Movie)
                .FirstOrDefault(ul => ul.UserListID == listID);

            return list;
        }

        public async Task AddToDefaultList(int listID, string title, string type, string userID)
        {
            Movie movie = await _movieHelper.GetMovie(title, type);

            if (movie == null)
            {
                movie = await _movieHandler.CreateMovie(title, type);
            }

            ListMovie listMovie = await _listMovieHelper.GetDefaultListMovieForUser(userID, listID);

            if (movie.Lists == null)
            {
                movie.Lists = new List<ListMovie>();
            }

            _context.ListMovies.Add(listMovie);
            movie.Lists.Add(listMovie);

            await _context.SaveChangesAsync();

            return;
        }

        public async Task AddCustomToList(int listID, string title, string type)
        {
            Movie movie = await _movieHelper.GetMovie(title, type);

            if (movie == null)
            {
                return;
            }

            ListMovie listMovie = await _listMovieHelper.GetListMovie(listID, movie.MovieID);

            if (movie.Lists == null)
            {
                movie.Lists = new List<ListMovie>();
            }

            _context.ListMovies.Add(listMovie);
            movie.Lists.Add(listMovie);

            await _context.SaveChangesAsync();

            return;
        }

        public async Task RemoveFromList(int listID, string title, string type)
        {
            Movie movie = await _movieHandler.GetExistingLocalMovie(title, type);
            
            if (movie == null)
            {
                return;
            }

            ListMovie listMovie = await _listMovieHelper.GetListMovie(listID, movie.MovieID);

            if (listMovie == null)
            {
                return;
            }

            _context.ListMovies.Remove(listMovie);
            movie.Lists.Remove(listMovie);

            await _context.SaveChangesAsync();

            return;
        }
    }
}
