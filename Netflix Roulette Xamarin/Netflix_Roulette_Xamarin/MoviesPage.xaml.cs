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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        private string Url = "http://netflixroulette.net/api/api.php?actor={0}";
        private HttpClient _client = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<Movies> _movies;

        public MoviesPage()
        {
            BindingContext = this;
            InitializeComponent();
            //ActivityRunning = false;
            //BindingContext = new MoviesPageViewModel();
        }

        public static readonly BindableProperty ActivityRunningProperty =
            BindableProperty.Create("ActivityRunning", typeof(bool), typeof(MoviesPage), false);
        public bool ActivityRunning
        {
            get
            {
                return (bool)GetValue(ActivityRunningProperty);
            }
            set
            {
                SetValue(ActivityRunningProperty, value);
            }

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

                ActivityRunning = false;
                return;
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                moviesListView.ItemsSource = null;
                await DisplayAlert("Error!", "Error.", "OK");
                ActivityRunning = false;
                return;
            }

            moviesExist.IsVisible = false;
            moviesListView.IsVisible = true;

            var content = await _client.GetStringAsync(stringParam);
            ActivityRunning = false;
            var movieList = JsonConvert.DeserializeObject <List<Movies>>(content);
            _movies = new ObservableCollection<Movies>(movieList);
            moviesListView.ItemsSource = _movies;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(String.IsNullOrEmpty(e.NewTextValue))
            {
                moviesListView.ItemsSource = null;
            }

            if(e.NewTextValue != e.OldTextValue && 
                e.NewTextValue.Trim().Length >= 5)
            {
                ActivityRunning = true;
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
