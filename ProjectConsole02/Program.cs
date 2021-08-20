using System;
using System.Linq;
using ProjectConsole02.Models;

namespace ProjectConsole02
{
  class Program
  {
    static void Main(string[] args)
    {
      using(var db = new MoviesDbContext())
      {
        // db.Movies.Add(new Movie {
        //   Title = "Test 01",
        //   LaunchYear = 2021,
        // });

        // db.Movies.Add(new Movie {
        //   Title = "Test 02",
        //   LaunchYear = 2021,
        //   Duration = 180,
        // });

        // db.SaveChanges();

        // db.Movies.OrderBy(m => m.LaunchYear).ToList().ForEach(m => 
        //   Console.WriteLine($"{m.Title} - {m.LaunchYear}")
        // );
        
        // var movie = db.Movies.Find(2);
        
        // if (movie != null) 
        // {
        //   movie.Synopsis = "Synopsis 02";
        //   db.SaveChanges();
        // }
        
        // var movie = db.Movies.Where(m => m.Title == "Test 01").FirstOrDefault();
        
        // if (movie != null) 
        // {
        //   Console.WriteLine($"{movie.Title} - {movie.LaunchYear}");
        // }

      //   var movie = db.Movies.Find(2);

      //   if (movie != null) 
      //   {
      //     db.Movies.Remove(movie);
      //     db.SaveChanges();
      //   }
      // }
    }
  }
}
