using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class RatingCreateDto
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5")]
        public int Rating { get; set; }
    }
}
