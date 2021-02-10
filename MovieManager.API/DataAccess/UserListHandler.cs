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

        public UserListHandler(MovieContext context)
        {
            _context = context;
            _movieHandler = new MovieHandler(_context);
            _listMovieHelper = new ListMovieHelper(_context);
        }

        public async Task AddToDefaultList(int listID, string title, string type, string userID)
        {
            Movie movie = _context.Movies.FirstOrDefault(m => m.Title == title && m.Type == type);

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

        public void AddToList(int listID)
        {
            //UserList ul = new UserList();
            //ul.
        }
    }
}
