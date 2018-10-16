using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> library = Library.GetList();

            Menu.DisplayMainMenu(library);
        }
    }
}
