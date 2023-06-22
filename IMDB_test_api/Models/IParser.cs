using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_test_api.Models
{
    public interface IParser
    {
       public Task<string> getJSON(string expression);
        public Task setMovieList(string expression, List<Movie> movieList);
    }
}
