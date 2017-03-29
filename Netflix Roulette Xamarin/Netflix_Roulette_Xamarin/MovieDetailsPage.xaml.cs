using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflix_Roulette_Xamarin
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsPage : ContentPage
    {
        private MovieService _service = new MovieService();
        public string MovieTitle { get; set; }

        public MovieDetailsPage(string movieTitle)
        {
            BindingContext = this;
            InitializeComponent();

            Title = MovieTitle = movieTitle;
            GetMovieDetails();
        }

        async private void GetMovieDetails()
        {
            var movie = await _service.GetMovie(MovieTitle);
            category.Text = movie?.Category;
            summary.Text = movie?.Summary;
            portrait.Text = movie?.Poster;
            moviePoster.Source = movie?.Poster;
        }        
    }    
}
