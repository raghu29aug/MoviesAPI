using MoviesAPI.Contexts;
using MoviesAPI.Entities;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MoviesAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _context;
        
        public MovieRepository(MoviesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> AddUpdateRating(RatingCreateDto rating)
        {
            if(_context.Ratings.Any(x => x.UserId == rating.UserId && x.MovieId == rating.MovieId))
            {
                var userRating = _context.Ratings.FirstOrDefault(x => x.UserId == rating.UserId && x.MovieId == rating.MovieId);
                userRating.Value = rating.Rating;
                await _context.SaveChangesAsync();
                return false;
            }
            else
            {
                _context.Ratings.Add(new Rating { UserId = rating.UserId, MovieId = rating.MovieId, Value = rating.Rating });
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<MoviesDto>> GetMovies(string title, int year, List<string> genre)
        {
            var movies = _context.Movies.AsQueryable();
            var genres = _context.Genres.AsQueryable();
            var moviegenres = _context.MovieGenres.AsQueryable();
            var ratings = _context.Ratings
                        .GroupBy(x => x.MovieId)
                        .Select(y => new { MovieId = y.Key, AvgRating = y.Average(z => z.Value) });

            if (!string.IsNullOrEmpty(title))
                movies = movies.Where(x => x.Title.Contains(title));
            if (year > 1900 && year < DateTime.Now.Year) //TODO: This is an assumption, can be handled in a better way
                movies = movies.Where(x => x.Year == year);

            var result1 = await (from m in movies
                                join mg in moviegenres
                                on m.Id equals mg.MovieId
                                join g in genres
                                on mg.GenredId equals g.Id
                                join r in ratings
                                on m.Id equals r.MovieId
                                select new {
                                    m.Id,
                                    m.Title,
                                    m.Year,
                                    m.RunningTime,
                                    g.Name,
                                    r.AvgRating
                                })
                          .ToListAsync();
              var result2 =  result1.GroupBy(x => new { x.Id, x.Title, x.Year, x.RunningTime, x.AvgRating })                          
                              .Select(y => new MoviesDto{ 
                                  Id = y.Key.Id, 
                                  Title = y.Key.Title, 
                                  YearOfRelease = y.Key.Year, 
                                  RunningTime = y.Key.RunningTime, 
                                  Genres = string.Join(",", y.Select(x => x.Name).Distinct()),
                                  AverageRating = CalculateRatingToClosestHalf(y.Key.AvgRating)
                              })
                              .ToList();

            return result2;
        }

        public async Task<List<MoviesDto>> Top5MoviesByAverageRatings()
        {
            var movies = _context.Movies.AsQueryable();
            var genres = _context.Genres.AsQueryable();
            var moviegenres = _context.MovieGenres.AsQueryable();
            var ratings = _context.Ratings
                        .GroupBy(x => x.MovieId)
                        .Select(y => new { MovieId = y.Key, AvgRating = y.Average(z => z.Value) })
                        .OrderByDescending(x => x.AvgRating)
                        .Take(5);

            var result1 = await (from m in movies
                                 join mg in moviegenres
                                 on m.Id equals mg.MovieId
                                 join g in genres
                                 on mg.GenredId equals g.Id
                                 join r in ratings
                                 on m.Id equals r.MovieId
                                 select new
                                 {
                                     m.Id,
                                     m.Title,
                                     m.Year,
                                     m.RunningTime,
                                     g.Name,
                                     r.AvgRating
                                 })
                          .ToListAsync();

            var result2 =  result1.GroupBy(x => new { x.Id, x.Title, x.Year, x.RunningTime, x.AvgRating })
                              .Select(y => new MoviesDto
                              {
                                  Id = y.Key.Id,
                                  Title = y.Key.Title,
                                  YearOfRelease = y.Key.Year,
                                  RunningTime = y.Key.RunningTime,
                                  Genres = string.Join(",", y.Select(x => x.Name).Distinct()),
                                  AverageRating = CalculateRatingToClosestHalf(y.Key.AvgRating)
                              })
                              .OrderByDescending(x => x.AverageRating)
                              .ThenBy(x => x.Title)
                              .ToList();

            return result2;
        }

        public async Task<List<MoviesDto>> Top5MoviesByUserHighestRatings(int userid)
        {
            var movies = _context.Movies.AsQueryable();
            var genres = _context.Genres.AsQueryable();
            var moviegenres = _context.MovieGenres.AsQueryable();
            var ratings = _context.Ratings
                        .Where(x => x.UserId == userid)
                        .GroupBy(x => new { x.UserId, x.MovieId})
                        .Select(y => new { UserId = y.Key.UserId, MovieId = y.Key.MovieId, AvgRating = y.Average(z => z.Value) })
                        .OrderByDescending(x => x.AvgRating)
                        .Take(5);

            var result1 = await (from m in movies
                           join mg in moviegenres
                           on m.Id equals mg.MovieId
                           join g in genres
                           on mg.GenredId equals g.Id
                           join r in ratings
                           on m.Id equals r.MovieId
                           select new
                           {
                               m.Id,
                               m.Title,
                               m.Year,
                               m.RunningTime,
                               g.Name,
                               r.AvgRating
                           })
                          .ToListAsync();

                var result2 = result1.GroupBy(x => new { x.Id, x.Title, x.Year, x.RunningTime, x.AvgRating })
                          .Select(y => new MoviesDto
                          {
                              Id = y.Key.Id,
                              Title = y.Key.Title,
                              YearOfRelease = y.Key.Year,
                              RunningTime = y.Key.RunningTime,
                              Genres = string.Join(",", y.Select(x => x.Name).Distinct()),
                              AverageRating = CalculateRatingToClosestHalf(y.Key.AvgRating)
                          })
                          .OrderByDescending(x => x.AverageRating)
                          .ThenBy(x => x.Title)
                          .ToList();

            return result2;
        }

        public async Task<bool> IsUserOrMoviePresent(RatingCreateDto rating)
        {
            var movies = _context.Movies.AsQueryable();
            var users = _context.Users.AsQueryable();

            return await movies.AnyAsync(x => x.Id == rating.MovieId) && await users.AnyAsync(x => x.Id == rating.UserId);
        }
        private double CalculateRatingToClosestHalf(double rating)
        {
            return Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2;
        }
    }
}
