
using MovieLibrary;
using MovieLibrary.Model;
using MovieLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieSerializationAssignment.Controller
{
    internal class MovieController
    {
        MovieManager movieManager;
        public MovieController() {
           movieManager= new MovieManager(ConfigurationManager.AppSettings["path"]);
           Start();
        }
        public void Start()
        {
              
            DisplayMenu();
            
        }
        private  void DisplayMenu()
        {

            while (true)
            {
                Console.WriteLine($" Movie Count:{movieManager.GetMovieCount()} /{movieManager.MAXMOVIE}");
                Console.Write("********************\n" +
                    "1. Add Movie\n" +
                    "2. Display All Movies Data\n" +
                    "3. Display Movie By Year\n" +
                    "4. Remove Movie By Name\n" +
                    "5. Remove All Movies\n" +
                    "6. Exit\n" +
                    "Enter Your Choice\n"

                    );
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            SetMovieDetails();
                            break;
                        case 2:
                            DisplayMovies();
                            break;
                        case 3:
                            DisplayMoviesByYear();
                            break;
                        case 4:
                            RemoveMoviesByName();
                            break;
                        case 5:
                            RemoveAllMovies();
                            break;
                        case 6:

                            Environment.Exit(0);

                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("--------------------\n");
                    Console.WriteLine(ex.Message);
                    Console.Write("--------------------\n");
                }
              

          
            }
        }
        private  void SetMovieDetails()
        {
            
            Console.WriteLine("Enter Movie Title:");
            string movieName = Console.ReadLine();
            Console.WriteLine("Enter Genere :");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter Director :");
            string directorName = Console.ReadLine();
            Console.WriteLine("Enter Year of Release:");
            int yearOfRelease = Convert.ToInt32(Console.ReadLine());

            try
            {
                movieManager.AddMovie(movieName, genre, directorName, yearOfRelease);
                Console.Write("--------------------\n");

                Console.WriteLine("Data is added successfully");
                Console.Write("--------------------\n");

            }
            catch (ListOverflowException lo)
            {
                Console.Write("--------------------\n");
                Console.WriteLine(lo.Message);
                Console.Write("--------------------\n");
            }
             
            
        }

        private void RemoveMoviesByName()
        {
            Console.WriteLine("Enter title of  movie to search");
            string movieName = Console.ReadLine();


            if (movieManager.ClearMovies(movieName))
            {
                Console.Write("--------------------\n");
                Console.WriteLine("removed movie successfully!");
                Console.Write("--------------------\n");
            }
            


        }
        private void DisplayMoviesByYear()
        {
            Console.WriteLine("Enter year of release of movie");
            int searchYear = Convert.ToInt32(Console.ReadLine());
            try { 
            List<Movie> searchMovies = movieManager.FindMovie(searchYear);
            
                foreach (Movie movie in searchMovies)
                {
                    Console.WriteLine($"Title: {movie.Title}\n" +
                         $"Genre: {movie.Genere}\n" +
                         $"Director: {movie.Director}\n" +
                         $"Year of Release: {movie.YearOfRelease}");
                }

            }
            catch(NullAccessException e)
            {
                Console.Write("--------------------\n");
                Console.WriteLine(e.Message);
                Console.Write("--------------------\n");
            }

        }

        public void RemoveAllMovies()
        {
            try
            {
                if (movieManager.DeleteAllMovies())
                {
                    Console.Write("--------------------\n");
                    Console.WriteLine(" Removed All Movies!");
                    Console.Write("--------------------\n");
                }
            }catch(NullAccessException e)
            {
                Console.Write("--------------------\n");
                Console.WriteLine(e.Message);
                Console.Write("--------------------\n");
            }
        }

        public void DisplayMovies()
        {

            try
            {
                List<Movie> movies = movieManager.GetMovies();


                Console.Write("--------------------\n");
                foreach (Movie movie in movies)
                {
                    Console.WriteLine($"Title: {movie.Title}\n" +
                        $"Genre: {movie.Genere}\n" +
                        $"Director: {movie.Director}\n" +
                        $"Year of Release: {movie.YearOfRelease}");
                    Console.Write("--------------------\n");

                }
            }catch(NullAccessException e)
            {
                Console.Write("--------------------\n");
                Console.WriteLine(e.Message);
                Console.Write("--------------------\n");
            }

        }



    }
}
