using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_test_api.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Actors { get; set; }
        public string ReleaseYear { get; set; }
        public int Rank { get; set; }

        public Movie() {}
    }
}
