using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Menu : Library
    {
        public static List<Movie> library;
        public static void DisplayMainMenu()
        {
            int selectedOption = 0;
            while (selectedOption != 9)
            {
                Console.Clear();
                Console.WriteLine("\n\n--------------------------------------------------");
                Console.WriteLine("|Hey                                             |\n|\t\t\t\t\t\t |");
                Console.WriteLine("|Welcome to MAIN MENU                            |\n|\t\t\t\t\t\t |");
                Console.WriteLine("|Choose an option:                               |");
                Console.WriteLine("|1. Display all the library                      |");
                Console.WriteLine("|2. Display info about chosen movie              |");
                Console.WriteLine("|3. Find a movie based on actor that played there|");
                Console.WriteLine("|4. Filter videos by genre and date range        |");
                Console.WriteLine("|5. Import from file: Excel, Xml, JSON           |");
                Console.WriteLine("|6. Export to file: Excel, Xml, JSON             |");
                Console.WriteLine("|9. Go out from menu                             |");
                Console.WriteLine("--------------------------------------------------");

                selectedOption = int.Parse(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        DisplayAllLibrary(library);
                        break;
                    case 2:
                        Movie.DisplayInfoAboutChosenMovie(library);
                        break;
                    case 3:
                        Actor.DisplayMovieBasedOnActor(library);
                        break;
                    case 4:
                        Movie.FilterMoviesGenreAndDateRange(library);
                        break;
                    case 5:
                        ImportFrom(library);
                        break;
                    case 6:
                        ExportTo(library);
                        break;
                    default:
                        Console.WriteLine("You fucked up");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void ExportTo(List<Movie> library)
        {
            int selectedOption = 0;
            while (selectedOption != 9)
            {
                Console.Clear();
                Console.WriteLine("\n\n--------------------------------------------------");
                Console.WriteLine("|Choose an option:                               |");
                Console.WriteLine("|0. Go back to MAIN MENU:                               |");
                Console.WriteLine("|1. Export to Excel                              |");
                Console.WriteLine("|2. Export to Xml                                |");
                Console.WriteLine("|3. Export to json                               |");
                Console.WriteLine("|9. Go out from menu                             |");
                Console.WriteLine("--------------------------------------------------");

                selectedOption = int.Parse(Console.ReadLine());

                switch (selectedOption)
                {
                    case 0:
                        DisplayMainMenu(library);
                        break;
                    case 1:
                        Library.ExportToExcel();
                        break;
                    case 2:
                        Library.ExportToXml();
                        break;
                    case 3:
                        Library.ExportToJson();
                        break;
                    default:
                        Console.WriteLine("You fucked up");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void ImportFrom(List<Movie> library)
        {
            var selectedOption = 0;
            while (selectedOption != 9)
            {
                Console.Clear();
                Console.WriteLine("\n\n------------------------------");
                Console.WriteLine("|Choose an option:               |");
                Console.WriteLine("|0. Go back to MAIN MENU:        |");
                Console.WriteLine("|1. Import from json             |");
                Console.WriteLine("|2. Import from Xml :(           |");
                Console.WriteLine("|3. Import from xlsx (implementation)           |");
                Console.WriteLine("|9. Go out from menu             |");
                Console.WriteLine("----------------------------------");


	            switch ((MenuOption)selectedOption)
	            {
		            case MenuOption.JsonImport:
						ExportToExcel();
			            break;
					case MenuOption.XmlImport:
			            break;
		            default:
			            throw new ArgumentOutOfRangeException();
	            }
            }
        }

	    public enum MenuOption
        {
            JsonImport =1,
            XmlImport =2
        }

    }
}
