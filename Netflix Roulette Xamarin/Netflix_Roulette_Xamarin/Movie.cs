using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Roulette_Xamarin
{
    public class Movie
    {
        [JsonProperty("show_title")]
        public string MovieTitle { get; set; }

        [JsonProperty("release_year")]
        public int ReleaseYear { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("poster")]
        public string Poster { get; set; }
    }
}
