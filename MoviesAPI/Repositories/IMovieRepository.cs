using MoviesAPI.Entities;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<List<MoviesDto>> GetMovies(string title, int year, List<string> genre);
        Task<List<MoviesDto>> Top5MoviesByAverageRatings();
        Task<List<MoviesDto>> Top5MoviesByUserHighestRatings(int userid);
        Task<bool> AddUpdateRating(RatingCreateDto rating);
        Task<bool> IsUserOrMoviePresent(RatingCreateDto rating);
    }
}
