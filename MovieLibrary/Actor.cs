using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace MovieLibrary
{
    [Serializable]
    public class Actor : Person, IPrintable
    {
        public static void DisplayMovieBasedOnActor(List<Movie> library)
        {
            Console.WriteLine("\n\n\nWhich movies have played the given actor?\nPlease type an actor: ");
            Library.DisplayActorsList(library);

            string selectedActor = Console.ReadLine();

            foreach (var movie in library)
            {
                foreach (var oneOfActors in movie.ListOfActors)
                {
                    if (oneOfActors.FirstName == selectedActor ||
                        oneOfActors.LastName == selectedActor ||
                        oneOfActors.FirstName + " " + oneOfActors.LastName == selectedActor)

                        Console.WriteLine($"{selectedActor} played in: {movie.Title}");
                }
            }
        }

	    public void Print()
	    {
		    Console.Write(" [{FirstName}  {LastName}] ");
	    }
	}
}