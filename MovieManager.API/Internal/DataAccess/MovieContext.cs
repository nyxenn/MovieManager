//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMApi.Internal.DataAccess
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<UserList> UserLists { get; set; }

        public DbSet<ListMovie> ListMovies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MoviePerson> MoviePeople { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Movie");

            builder.Entity<Movie>().ToTable("Movies");
            builder.Entity<Genre>().ToTable("Genres");
            builder.Entity<Person>().ToTable("People");
            builder.Entity<UserList>().ToTable("UserLists");

            builder.Entity<ListMovie>().HasKey(prop => new { prop.UserListID, prop.MovieID });
            builder.Entity<ListMovie>().HasOne(a => a.UserList).WithMany(b => b.Movies).HasForeignKey(a => a.UserListID);
            builder.Entity<ListMovie>().HasOne(a => a.Movie).WithMany(b => b.Lists).HasForeignKey(a => a.MovieID);

            builder.Entity<MovieGenre>().HasKey(prop => new { prop.MovieID, prop.GenreID });
            builder.Entity<MovieGenre>().HasOne(a => a.Movie).WithMany(b => b.Genres).HasForeignKey(a => a.MovieID);
            builder.Entity<MovieGenre>().HasOne(a => a.Genre).WithMany(b => b.Movies).HasForeignKey(a => a.GenreID);

            builder.Entity<MoviePerson>().HasKey(prop => new { prop.MovieID, prop.PersonID });
            builder.Entity<MoviePerson>().HasOne(a => a.Movie).WithMany(b => b.People).HasForeignKey(a => a.MovieID);
            builder.Entity<MoviePerson>().HasOne(a => a.Person).WithMany(b => b.Movies).HasForeignKey(a => a.PersonID);
        }
    }
}
