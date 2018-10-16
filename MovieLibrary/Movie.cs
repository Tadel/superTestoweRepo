using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using MovieLibrary;

namespace MovieLibrary
{
    [Serializable]
    public class Movie
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("director")]
        public Director Director { get; set; }
       
        [XmlElement("dateOfProduction")]
        public DateTime DateOfProduction { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }

        [XmlArray("movie")]
        [XmlArrayItem("listOfActors", typeof(Actor))]
        public List<Actor> ListOfActors;


	    public void Print()
	    {
		    Console.WriteLine("Title: " + Title);
		    Director.Print();

			Console.WriteLine("DateOfProduction: " + DateOfProduction.Date);
		    Console.WriteLine("Genre: " + Genre);
		    Console.Write("Actors: ");
		    foreach (var actor in ListOfActors)
		    {
			    actor.Print();
		    }
		}
		public static void DisplayInfoAboutChosenMovie(List<Movie> library)
        {
            Console.WriteLine("\nChoose movie from list below:");
            Library.DisplayMoviesList(library);

            Console.WriteLine();
            string choosenMovie = Console.ReadLine();

            foreach (var movie in library)
            {
                if (movie.Title == choosenMovie)
                {
                    Console.WriteLine($"\nTitle: {movie.Title}");
                    Console.WriteLine($"Director: {movie.Director}");
                    Console.WriteLine($"Genre: {movie.Genre}");
                    Console.WriteLine($"Date of production: {movie.DateOfProduction}");
                    Console.WriteLine($"Genre: {movie.Genre}");

                    Console.WriteLine("Actress: ");
                    foreach (var oneOfActors in movie.ListOfActors)
                    {
                        Console.WriteLine($"\t{oneOfActors.FirstName} {oneOfActors.LastName}");
                    }
                }
            }
        }

        public static void FilterMoviesGenreAndDateRange(List<Movie> library)
        {
            List<Movie> listOfMovies = new List<Movie>();
            Console.WriteLine("\nChoose genre from the list below:");
            Library.DisplayGenreList(library);
            Console.WriteLine();
            string chosenGenre = Console.ReadLine();

            Console.WriteLine("\nType the beginning (oldest day) of date range in format DDmmYYYY");
            DateTime beginningOfRange = DateTime.ParseExact(Console.ReadLine(), "yyyyMMdd",
                CultureInfo.InvariantCulture);

            Console.WriteLine("\nType the end (youngest) of date range in format DDmmYYYY");
            DateTime endOfRange = DateTime.ParseExact(Console.ReadLine(), "yyyyMMdd",
                CultureInfo.InvariantCulture);

            foreach (var movie in library)
            {
                if (movie.Genre == chosenGenre &&
                    (movie.DateOfProduction > beginningOfRange && movie.DateOfProduction < endOfRange))
                {
                    listOfMovies.Add(movie);
                }
            }

            Console.WriteLine();
            int i = 0;
            foreach (var movie in listOfMovies)
            {
                i++;
                Console.WriteLine($"{i}. {movie.Title}");
            }
        }
    }
}
