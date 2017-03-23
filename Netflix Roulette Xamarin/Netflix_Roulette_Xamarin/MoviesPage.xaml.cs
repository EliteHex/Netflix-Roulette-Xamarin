using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflix_Roulette_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        private string Url = "http://netflixroulette.net/api/api.php?actor={0}";
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Movies> _movies;

        public MoviesPage()
        {
            InitializeComponent();
            //BindingContext = new MoviesPageViewModel();
        }

        protected override void OnAppearing()
        {
            //var response = await _client.GetAsync(String.Format(Url, "Gibson"));
            

            base.OnAppearing();
        }

        async private void SearchForMovies(string appendValue)
        {
            var stringParam = String.Format(Url, appendValue); 
            var response = await _client.GetAsync(stringParam);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                moviesListView.IsVisible = false;
                moviesListView.ItemsSource = null;
                moviesExist.IsVisible = true;

                //await DisplayAlert("Oops!", "No movies were found matching the search", "OK");
                return;
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                moviesListView.ItemsSource = null;
                await DisplayAlert("Error!", "Error.", "OK");
                return;
            }

            var content = await _client.GetStringAsync(stringParam);
            var movieList = JsonConvert.DeserializeObject <List<Movies>>(content);
            _movies = new ObservableCollection<Movies>(movieList);
            moviesListView.ItemsSource = _movies;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue != e.OldTextValue && 
               !String.IsNullOrEmpty(e.NewTextValue) &&
               e.NewTextValue.Length >= 5)
            {
                SearchForMovies(e.NewTextValue);
            }
        }
    }

    public class Movies
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
        public ImageSource Poster { get; set; }
    }
}
