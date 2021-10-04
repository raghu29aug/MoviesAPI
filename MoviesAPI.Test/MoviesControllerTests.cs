using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesAPI.Controllers;
using MoviesAPI.Models;
using MoviesAPI.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Test
{
    [TestFixture]
    public class MoviesControllerTests
    {
        private Mock<IMovieRepository> _moviesRepo;
        private MoviesController _moviesController;

        public MoviesControllerTests()
        {
            _moviesRepo = new Mock<IMovieRepository>();            
        }

        [Test]
        public void GetMovies_When_Correct_Filter_Then_Return_Response()
        {
            var movieFilter = new MovieFilter { Title = "Test1", Genre = new HashSet<string> { "Genre1" }, Year = 2000 };
            var moviesList = new List<MoviesDto> { 
                new MoviesDto { Id = 1, Title = "Test1", AverageRating = 4, YearOfRelease = 2000, Genres = "Genre1" } 
            };

            _moviesRepo.Setup(x => x.GetMovies(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<List<string>>())).ReturnsAsync(moviesList);
            _moviesController = new MoviesController(_moviesRepo.Object);

            var response = (ObjectResult)_moviesController.Get(movieFilter).Result;

            Assert.IsInstanceOf<List<MoviesDto>>(response.Value);
        }
        [Test]
        public void GetMovies_When_Wrong_Filter_Then_Return_404_NoResults()
        {
            var movieFilter = new MovieFilter { Title = "Test2", Genre = new HashSet<string> { "Genre5" }, Year = 2010 };
            var moviesList = new List<MoviesDto> { };

            _moviesRepo.Setup(x => x.GetMovies(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<List<string>>())).ReturnsAsync(moviesList);

            ObjectResult response = (ObjectResult)_moviesController.Get(movieFilter).Result;
                        
            Assert.IsTrue(response.Value.Equals("No movies found for the given criteria"));
            Assert.IsTrue(response.StatusCode == 404);
        }

        [Test]
        public void GetMovies_When_No_Filter_Then_Return_BadRequest()
        {
            var movieFilter = new MovieFilter { };
            var moviesList = new List<MoviesDto> { 
                new MoviesDto { Id = 1, Title = "Test1", AverageRating = 4, YearOfRelease = 2000, Genres = "Genre1" } 
            };

            _moviesRepo.Setup(x => x.GetMovies(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<List<string>>())).ReturnsAsync(moviesList);

            ObjectResult response = (ObjectResult)_moviesController.Get(movieFilter).Result;

            Assert.IsTrue(response.Value.Equals("Please provide a filter Title, Year and(or) Genre"));
            Assert.IsTrue(response.StatusCode == 400);
        }
    }
}
