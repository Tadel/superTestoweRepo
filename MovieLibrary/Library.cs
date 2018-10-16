using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Excel;
using System.Web.Script.Serialization;
using MovieLibrary;

namespace MovieLibrary
{
    [Serializable]
    [XmlRoot(Namespace = "movie")]
    public class Library
    {
        private static List<string> listOfFiles = new List<string>();
	    public static List<Movie> AllMovies { get; }

	    public void Print()
	    {
		    foreach (var movie in GetList())
		    {
			    movie.Print();
		    }
	    }
        public static void DisplayActorsList(List<Movie> library)
        {
            foreach (var movie in library)
            {
                foreach (var oneOfActors in movie.ListOfActors)
                {
                    Console.WriteLine($"\t{oneOfActors.FirstName} {oneOfActors.LastName}");
                }
            }
        }

        public static void DisplayAllLibrary(List<Movie> library)
        {
            foreach (var movie in library)
            {
                Console.WriteLine(movie.Title);
                Console.WriteLine(movie.Director.FirstName + " " + movie.Director.LastName);
                Console.WriteLine(movie.DateOfProduction);
                Console.WriteLine(movie.Genre);

                foreach (var oneOfActors in movie.ListOfActors)
                {
                    Console.WriteLine("\t" + oneOfActors.FirstName + ", " + oneOfActors.LastName);
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine("\nPress 0 to go back to main menu");
            int selectedOption = int.Parse(Console.ReadLine());
            if (selectedOption == 0) Menu.DisplayMainMenu(library);
        }

        public static List<string> DisplayDirectoryContent()
        {
            Console.Clear();
            string[] filePaths = Directory.GetFiles(".\\Files\\");
            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];
                Console.WriteLine($"{i}. "+Path.GetFileName(path));
                listOfFiles.Add(path);
            }

            return listOfFiles;
        }

        public static void DisplayGenreList(List<Movie> library)
        {
            List<string> listOfGengres = new List<string>();

            foreach (var movie in library)
            {
                if (!listOfGengres.Contains(movie.Genre))
                {
                    listOfGengres.Add(movie.Genre);
                }
            }

            int i = 0;
            foreach (var gengre in listOfGengres)
            {
                i++;
                Console.WriteLine($"{i}. {gengre}");
            }
        }

        public static void DisplayMoviesList(List<Movie> library)
        {
            int i = 0;
            foreach (var movie in library)
            {
                i++;
                Console.WriteLine($"{movie.Title}");
            }
        }

        public static void ExportToExcel()
        {
            Application excel = new Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            
            Workbook excelWorkBook = excel.Workbooks.Add(Type.Missing);
            Worksheet excelSheet = (Worksheet)excelWorkBook.ActiveSheet;
            excelSheet.Name = "Test work sheet";

            excelSheet.Cells[1, 1] = "Title";
            excelSheet.Cells[1, 2] = "Director";
            excelSheet.Cells[1, 3] = "DateOfProduction";
            excelSheet.Cells[1, 4] = "Genre";
            excelSheet.Cells[1, 5] = "ListOfActors";

            int movieNumber = 2;
            foreach (var movie in GetList())
            {
                excelSheet.Cells[movieNumber, 1] = movie.Title;
                excelSheet.Cells[movieNumber, 2] = movie.Director.FirstName + " " + movie.Director.LastName;
                excelSheet.Cells[movieNumber, 3] = movie.DateOfProduction;
                excelSheet.Cells[movieNumber, 4] = movie.Genre;

                int actorsStartsFromFieldNumber = 5;
                foreach (var actor in movie.ListOfActors)
                {
                    excelSheet.Cells[movieNumber, 5] = actor.FirstName + " " + actor.LastName + "\n";
                    actorsStartsFromFieldNumber++;
                }

                movieNumber++;
            }

            //resize the columns
            Range excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[movieNumber-1, 5]];
            excelCellrange.EntireColumn.AutoFit();

            Console.WriteLine("\nSet a file name:");
            string filename = Console.ReadLine();
            excelSheet.SaveAs(filename);
            excelWorkBook.Close();
        }

        public static void ExportToJson()
        {
            var jsonFile = new JavaScriptSerializer().Serialize(GetList());

            Console.WriteLine("\nSet a file name:");
            string filename = Console.ReadLine();
            File.WriteAllText($".\\Files\\{filename}.json", jsonFile);
        }

        public static void ExportToXml()
        {
            Console.WriteLine("\nSet a file name:");
            string filename = Console.ReadLine();

            string xmlInString = ObjectToXml(GetList());
            File.WriteAllText($".\\Files\\{filename}.xml", xmlInString);
        }

        public static string ObjectToXml<T>(T objectToSerialise)
        {
            StringWriter output = new StringWriter(new StringBuilder());
            XmlSerializer xs = new XmlSerializer(objectToSerialise.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("MyNs", "MovieLibrary");
            xs.Serialize(output, objectToSerialise, ns);
            return output.ToString();
        }

        public static List<Movie> GetList()
        {
            return ImportJson();
        }

        public static List<Movie> ImportJson()
        {
            return JsonConvert.DeserializeObject<List<Movie>>(ImportTextOfJson());
        }

        public static Library ImportXml()
        {
            DisplayDirectoryContent();

            Console.WriteLine("\nChoose one of files below by typing the full name:");
            string filename = Console.ReadLine();

            StreamReader reader = new StreamReader(".\\Files\\" + filename);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "MovieLibrary";
            xRoot.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(Library));
            return (Library)serializer.Deserialize(reader);
        }

        internal static void ImportExcel()
        {
            Console.WriteLine("Not implemented yet");
        }

        public static string ImportTextOfJson()
        {
            StreamReader file = new StreamReader(@".\Files\Filmy-DoNotChange.json");
            return file.ReadToEnd();
        }
    }
}
