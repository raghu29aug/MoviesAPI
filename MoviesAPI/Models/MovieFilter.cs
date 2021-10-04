using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class MovieFilter
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public HashSet<string> Genre { get; set; } = new HashSet<string>();
    }
}
