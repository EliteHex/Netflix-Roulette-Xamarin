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
            MovieTitle = movieTitle;
            InitializeComponent();
            //BindingContext = new ContentPageViewModel();
        }

        async private void GetMovieDetails()
        {
            var movie = await _service.GetMovie(MovieTitle);
        }

        protected override void OnAppearing()
        {


            base.OnAppearing();
        }
    }

    //class MovieDetailsPageViewModel : INotifyPropertyChanged
    //{
    //    public MovieDetailsPageViewModel()
    //    {
    //        IncreaseCountCommand = new Command(IncreaseCount);
    //    }

    //    int count;

    //    string countDisplay = "You clicked 0 times.";
    //    public string CountDisplay
    //    {
    //        get { return countDisplay; }
    //        set { countDisplay = value; OnPropertyChanged(); }
    //    }

    //    public ICommand IncreaseCountCommand { get; }

    //    void IncreaseCount() =>
    //        CountDisplay = $"You clicked {++count} times";


    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    //}
}
