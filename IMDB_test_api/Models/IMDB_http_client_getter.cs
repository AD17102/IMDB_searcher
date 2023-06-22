using Avalonia.Animation;
using Avalonia.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace IMDB_test_api.Models
{
    internal class IMDB_http_client_getter: IParser
    {

        public async Task<string> getJSON(string expression)
        {
            //just return description if the query is empty at first
            if(expression == "") {

                return await Task<string>.Run(() => "{\"description\": []}"); 
            }

            HttpClient client = new HttpClient();
            var site = $"https://search.imdbot.workers.dev/?q=";
            var siteexp = Uri.EscapeDataString(expression);   

            var response = await client.GetAsync(site + siteexp);
            response.EnsureSuccessStatusCode();     
            return await response.Content.ReadAsStringAsync();
        }


        public async Task setMovieList(string expression, List<Movie> movieList)
        {
            movieList.Clear();
            var json =  await getJSON(expression);
            var jsonFile = JsonDocument.Parse(json);
            
            var Movies = jsonFile.RootElement.GetProperty("description").EnumerateArray();

            string title;
            string year;
            Int32 rank;
            string actors;
            string photo; 



                while (Movies.MoveNext())
                {
                var currentMovie = Movies.Current;
                try {     
                         title = currentMovie.GetProperty("#TITLE").ToString();
                    }
                    catch (System.Collections.Generic.KeyNotFoundException){
                    title = "No title"; 
                }

                try{
                    year = currentMovie.GetProperty("#YEAR").ToString();
                }
                catch (System.Collections.Generic.KeyNotFoundException){
                    year = "No year";
                }

                try{
                    rank = currentMovie.GetProperty("#RANK").GetInt32();
                }

                catch (System.Collections.Generic.KeyNotFoundException){
                    rank = 0;
                }

                try {
                    actors = currentMovie.GetProperty("#ACTORS").ToString();

                }
                catch (System.Collections.Generic.KeyNotFoundException){
                    actors = "No Actors";
                }

                try{
                    photo = currentMovie.GetProperty("#IMG_POSTER").ToString();

                }
                catch (System.Collections.Generic.KeyNotFoundException){
                    photo = Environment.CurrentDirectory + "\\..\\..\\..\\Assets\\IMG_not_found.jpg";
                }
                movieList.Add(new Movie { Name = title, Rank = rank, ReleaseYear = year, Actors = actors, Photo = photo });



            };

                

              
          


        }

    }
}
