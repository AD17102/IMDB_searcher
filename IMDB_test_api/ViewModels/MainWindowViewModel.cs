
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using IMDB_test_api.Models;
using IMDB_test_api.Views;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows;
using System.Media;
using System.IO;

namespace IMDB_test_api.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IParser Parse { get;  }

        public MainWindowViewModel(IParser parse)
        {
            this.Parse = parse;

        }


        /// Variables for the search input and index for the flipLeft and flipRight function

        public List<Movie> movies = new List<Movie>();
        SoundPlayer player = new SoundPlayer( Environment.CurrentDirectory + "\\..\\..\\..\\Assets\\shingles_laugh.wav");
       
        private int index = 0;
        


        ///Display values (movie title, actors, year photo)

        private string _movieYear = "YEAR: ";
        public string movieYear {
            get => _movieYear;
            set => this.RaiseAndSetIfChanged(ref _movieYear, value); 
        }



        private string _movieActors = "ACTORS:";
        public string movieActors
        {
            get => _movieActors;
            set => this.RaiseAndSetIfChanged(ref _movieActors, value); 
        }





        private string _image = ""; 
        public string image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);    

        }




        private string _movieTitle = "TITLE";
        public string movieTitle
        {
            get => _movieTitle;
            set => this.RaiseAndSetIfChanged(ref _movieTitle, value);   

        }

        private string _rank = "RANK"; 
        public string movieRank
        {
            get => _rank;
            set => this.RaiseAndSetIfChanged(ref _rank, value);
        }



        private string query = ""; 
        public string StringQuery
        {
            get => query;   
            set => this.RaiseAndSetIfChanged(ref query, value);
        }
     
     
        private void SetEmptyMovieList()
        {
            
            movieTitle = "YOU FOOL ";
            movieYear = "420BCE"; 
            movieActors = "WHERE'S YOUR QUERY??";
            image = "file://" + Environment.CurrentDirectory + "/../../../Assets/NO_query.jpg";

            movieRank = "69"; 
            player.Play(); 


        }
        //Private Method setDisplay
        private void setDisplay()
        {
            ///make sure that when the user tries to flip through an empty list that it doesn't allow him to do that. 
            try
            {
                movieTitle = movies[index].Name;
                movieActors = movies[index].Actors;
                movieYear =movies[index].ReleaseYear;
                image = movies[index].Photo;
                movieRank = movies[index].Rank.ToString(); 
            }
            catch (System.ArgumentOutOfRangeException)
            {

                SetEmptyMovieList();
            }
        }
        public  async void searchMovie()
        { 

            try
            { 

                await Parse.setMovieList(StringQuery, movies);
                index = 0; 

                setDisplay();
                
             


            }
            catch (System.ArgumentOutOfRangeException) {

                SetEmptyMovieList();
            }


        }
   
        public void FlipLeft()
        {
            index--; 
            if(index < 0)
            {
                index = movies.Count-1;
            }
            setDisplay();
        }
        public void FlipRight()
        {
            index++; 
            if (index > movies.Count-1) {
                index = 0; 
            }
            setDisplay();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

    }
}