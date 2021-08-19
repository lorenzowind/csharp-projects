using ProjectConsole02.Models;

namespace ProjectConsole02
{
  class Program
  {
    static void Main(string[] args)
    {
      using(var db = new MoviesDbContext())
      {
        db.Movies.Add(new Movie {
          Title = "Test 01",
          LaunchYear = 2021,
        });

        db.Movies.Add(new Movie {
          Title = "Test 02",
          LaunchYear = 2021,
          Duration = 180,
        });

        db.SaveChanges();
      }
    }
  }
}
