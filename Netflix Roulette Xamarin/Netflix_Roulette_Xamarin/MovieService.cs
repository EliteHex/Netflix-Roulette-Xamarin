using ModernHttpClient;
using Netflix_Roulette_Xamarin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Roulette_Xamarin
{
    public class MovieService
    {
        private string Url = "http://netflixroulette.net/api/api.php?{0}{1}";
        private HttpClient _client = new HttpClient(new NativeMessageHandler());

        public async Task<IEnumerable<Movie>> FindMoviesByActor(string actor)
        {
            var stringParam = String.Format(Url, "actor=", actor);
            var response = await _client.GetAsync(stringParam);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Enumerable.Empty<Movie>();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);
            return movieList;
        }

        public async Task<Movie> GetMovie(string title)
        {
            var stringParam = String.Format(Url, "title=", title);
            var response = await _client.GetAsync(stringParam);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new Movie();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<Movie>(content);
            return movieList;
        }
    }
}
