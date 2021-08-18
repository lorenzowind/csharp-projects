using System;
using System.Linq;

namespace ProjectConsole01
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var db = new BloggingContext())
      {
        // db.Add(new Blog { Url = "test 1" });
        // db.Add(new Blog { Url = "test 2" });

        // db.SaveChanges();

        db.Blogs.OrderBy(blog => blog.BlogId).ToList().ForEach(
          blog => 
            Console.WriteLine($"Id: {blog.BlogId}, Url: {blog.Url}"
        ));
      }
    }
  }
}
