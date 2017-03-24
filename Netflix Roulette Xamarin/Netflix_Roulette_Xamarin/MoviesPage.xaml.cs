using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;

namespace Netflix_Roulette_Xamarin
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        private ObservableCollection<Movie> _movies;
        private MovieService _service = new MovieService();

        public MoviesPage()
        {
            BindingContext = this;
            InitializeComponent();
            //ActivityRunning = false;
            //BindingContext = new MoviesPageViewModel();
        }

        public static readonly BindableProperty IsSearchingProperty =
            BindableProperty.Create("IsSearching", typeof(bool), typeof(MoviesPage), false);
        public bool IsSearching
        {
            get
            {
                return (bool)GetValue(IsSearchingProperty);
            }
            set
            {
                SetValue(IsSearchingProperty, value);
            }

        }

        async private void SearchForMovies(string appendValue)
        {
            try
            {
                var movies = await _service.FindMoviesByActor(appendValue);
                moviesListView.ItemsSource = movies;
                moviesListView.IsVisible = movies.Any();
                notFound.IsVisible = !moviesListView.IsVisible;
            }
            catch(Exception)
            {
                await DisplayAlert("Error", "There was an issue retrieving data.", "OK");
            }
            finally
            {
                IsSearching = false;
            }

            //if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{
            //    moviesListView.IsVisible = false;
            //    moviesListView.ItemsSource = null;
            //    moviesExist.IsVisible = true;

            //    ActivityRunning = false;
            //    return;
            //}
            //else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //{
            //    moviesListView.ItemsSource = null;
            //    await DisplayAlert("Error!", "Error.", "OK");
            //    ActivityRunning = false;
            //    return;
            //}

            //moviesExist.IsVisible = false;
            //moviesListView.IsVisible = true;

            //var content = await _client.GetStringAsync(stringParam);
            //ActivityRunning = false;
            //var movieList = JsonConvert.DeserializeObject <List<Movie>>(content);
            //_movies = new ObservableCollection<Movie>(movieList);
            //moviesListView.ItemsSource = _movies;
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

    
}
