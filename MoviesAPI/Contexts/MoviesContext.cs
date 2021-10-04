using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Contexts
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            //Database.EnsureCreated(); //To Create the DB on startup if not already created
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Movies Data
            modelBuilder.Entity<Movie>()
               .Property(p => p.Id)
               .UseIdentityColumn(10, 1);

            modelBuilder.Entity<Movie>()
                .HasData(
                new Movie { Id = 1, Title = "Die Hard", Year = 2000, RunningTime = 130 },
                new Movie { Id = 2, Title = "Avengers Endgame", Year = 2018, RunningTime = 120 },
                new Movie { Id = 3, Title = "Terminator", Year = 1998, RunningTime = 180 },
                new Movie { Id = 4, Title = "Titanic", Year = 1998, RunningTime = 200 },
                new Movie { Id = 5, Title = "Hangover", Year = 1994, RunningTime = 150 },
                new Movie { Id = 6, Title = "Matrix", Year = 2005, RunningTime = 180 },
                new Movie { Id = 7, Title = "Avatar", Year = 2010, RunningTime = 170 }
                );

            //Genre Data
            modelBuilder.Entity<Genre>()
                .Property(p => p.Id)
                .UseIdentityColumn(10, 1);

            modelBuilder.Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Comedy"},
                new Genre { Id = 2, Name = "Romance" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Action" },
                new Genre { Id = 5, Name = "Thriller" }
                );

            //MovieGenre
            modelBuilder.Entity<MovieGenre>()
                .Property(p => p.Id)
                .UseIdentityColumn(100, 1);

            modelBuilder.Entity<MovieGenre>()
                .HasData(
                new MovieGenre { Id = 1, MovieId = 1, GenredId = 4},
                new MovieGenre { Id = 2, MovieId = 1, GenredId = 5 },
                new MovieGenre { Id = 3, MovieId = 2, GenredId = 3 },
                new MovieGenre { Id = 4, MovieId = 2, GenredId = 4 },
                new MovieGenre { Id = 5, MovieId = 2, GenredId = 5 },
                new MovieGenre { Id = 6, MovieId = 3, GenredId = 4 },
                new MovieGenre { Id = 7, MovieId = 3, GenredId = 5 },
                new MovieGenre { Id = 8, MovieId = 4, GenredId = 2 },
                new MovieGenre { Id = 9, MovieId = 4, GenredId = 3 },
                new MovieGenre { Id = 10, MovieId = 4, GenredId = 4 },
                new MovieGenre { Id = 11, MovieId = 5, GenredId = 1 },
                new MovieGenre { Id = 12, MovieId = 5, GenredId = 3 },
                new MovieGenre { Id = 13, MovieId = 5, GenredId = 4 },
                new MovieGenre { Id = 14, MovieId = 6, GenredId = 4 },
                new MovieGenre { Id = 15, MovieId = 7, GenredId = 5 }
                );

            //User Data
            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .UseIdentityColumn(10, 1);

            modelBuilder.Entity<User>()
                .HasData(
                new User { Id = 1, FirstName = "John", LastName = "Smith"},
                new User { Id = 2, FirstName = "Ron", LastName = "Swanson" },
                new User { Id = 3, FirstName = "Michael", LastName = "Scott" },
                new User { Id = 4, FirstName = "Ross", LastName = "Geller" },
                new User { Id = 5, FirstName = "Jack", LastName = "Ryan" }
                );

            //Ratings Data
            modelBuilder.Entity<Rating>()
                .Property(p => p.Id)
                .UseIdentityColumn(10, 1);

            modelBuilder.Entity<Rating>()
                .HasData(
                new Rating { Id = 1, UserId = 1, MovieId = 1, Value = 4 },
                new Rating { Id = 2, UserId = 1, MovieId = 2, Value = 5 },
                new Rating { Id = 3, UserId = 1, MovieId = 3, Value = 3 },

                new Rating { Id = 4, UserId = 2, MovieId = 2, Value = 2 },
                new Rating { Id = 5, UserId = 2, MovieId = 3, Value = 4 },
                new Rating { Id = 6, UserId = 2, MovieId = 4, Value = 3 },                
                new Rating { Id = 8, UserId = 2, MovieId = 5, Value = 3 },
                new Rating { Id = 9, UserId = 2, MovieId = 6, Value = 1 },

                new Rating { Id = 10, UserId = 3, MovieId = 3, Value = 3 },
                new Rating { Id = 11, UserId = 3, MovieId = 4, Value = 4 },
                new Rating { Id = 12, UserId = 3, MovieId = 5, Value = 5 },

                new Rating { Id = 13, UserId = 4, MovieId = 4, Value = 5 },
                new Rating { Id = 14, UserId = 4, MovieId = 5, Value = 3 },
                new Rating { Id = 15, UserId = 4, MovieId = 6, Value = 1 },

                new Rating { Id = 16, UserId = 5, MovieId = 1, Value = 5 },
                new Rating { Id = 17, UserId = 5, MovieId = 2, Value = 3 },
                new Rating { Id = 18, UserId = 5, MovieId = 3, Value = 5 },
                new Rating { Id = 19, UserId = 5, MovieId = 5, Value = 5 },
                new Rating { Id = 20, UserId = 5, MovieId = 6, Value = 4 },
                new Rating { Id = 21, UserId = 5, MovieId = 7, Value = 5 }
                );
        }
    }
}
