using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class MovieGenre
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int GenredId { get; set; }
        public Genre Genre { get; set; }

    }
}
