using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public class Director : Person
    {
	    public void Print()
	    {
		    Console.WriteLine($"Director:  {FirstName}  {LastName}");
	    }
    }
}
