using Microsoft.EntityFrameworkCore;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMApi.Helpers
{
    public class ListMovieHelper
    {
        private readonly MovieContext _context;

        public ListMovieHelper(MovieContext context)
        {
            _context = context;
        }

        public async Task<ListMovie> GetDefaultListMovieForUser(string userID, int listID)
        {
            List<UserList> defaultLists = await _context.UserLists.Where(ul => ul.UserID == userID && ul.IsDefault).ToListAsync();
            UserList defaultList = null;
            ListMovie listMovie = null;

            foreach (var dl in defaultLists)
            {
                listMovie = await _context.ListMovies.FirstOrDefaultAsync(lm => lm.UserListID == dl.UserListID);
                if (listMovie != null)
                {
                    defaultList = dl;
                    break;
                }
            }

            if (listMovie != null)
            {
                _context.ListMovies.Remove(listMovie);   
            }

            listMovie = new ListMovie();
            listMovie.UserListID = listID;
            _context.ListMovies.Add(listMovie);

            return listMovie;
        }

        public async Task<ListMovie> GetListMovie(int listID, int movieID)
        {
            ListMovie listMovie =
                await _context.ListMovies
                .FirstOrDefaultAsync(lm => lm.UserListID == listID && lm.MovieID == movieID);

            return listMovie;
        }
    }
}
