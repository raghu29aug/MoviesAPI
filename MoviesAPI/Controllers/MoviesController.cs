using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesAPI.Models;
using MoviesAPI.Repositories;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;        
        public MoviesController(IMovieRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MovieFilter movieFilter)
        {
            if (string.IsNullOrEmpty(movieFilter.Title) && movieFilter.Year <= 1900 && (movieFilter.Genre == null || movieFilter.Genre.Count == 0))
                return BadRequest("Please provide a filter Title, Year and(or) Genre");

            var moviesList = await _repository.GetMovies(movieFilter.Title, movieFilter.Year, movieFilter.Genre.ToList());
            if (moviesList.Count() == 0)
                return NotFound("No movies found for the given criteria");

            return Ok(moviesList);
        }

        [HttpGet]
        [Route("top5movies")]
        public async Task<IActionResult> GetTop5MoviesByAverageRatings()
        {            
            var moviesList = await _repository.Top5MoviesByAverageRatings();
            if (moviesList.Count() == 0)
                return NotFound("No movies found for the given criteria");

            return Ok(moviesList);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var moviesList = await _repository.Top5MoviesByUserHighestRatings(id);
            if (moviesList.Count() == 0)
                return NotFound("No movies found for the given criteria");

            return Ok(moviesList);
        }

        [HttpPost]        
        public async Task<IActionResult> Post([FromBody]RatingCreateDto rating)
        {
            if (! await _repository.IsUserOrMoviePresent(rating))
                return NotFound("Either User or Movie or Both are not present.");

            if (rating.Rating < 1 || rating.Rating > 5)
                return BadRequest("Please provide a Rating between 1 and 5");

            await _repository.AddUpdateRating(rating);
            return Ok();
        }
    }
}
