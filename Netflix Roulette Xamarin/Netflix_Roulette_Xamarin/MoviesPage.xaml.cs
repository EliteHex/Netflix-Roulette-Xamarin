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
        //private ObservableCollection<Movie> _movies;
        private MovieService _service = new MovieService();

        public MoviesPage()
        {
            BindingContext = this;
            InitializeComponent();
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
            catch (Exception)
            {
                await DisplayAlert("Error", "There was an issue retrieving data.", "OK");
            }
            finally
            {
                IsSearching = false;
            }
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
                IsSearching = true;
                SearchForMovies(e.NewTextValue);
            }
        }
    }

    
}
