using Microsoft.EntityFrameworkCore;
using MMApi.Internal.DataAccess;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMApi.Helpers
{
    class MovieHelper
    {
        private readonly MovieContext _context;

        public MovieHelper(MovieContext context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovie(string title, string type)
        {
            Movie movie =
                await _context.Movies
                .FirstOrDefaultAsync(m => m.Title == title && m.Type== type);

            return movie;
        }
    }
}
