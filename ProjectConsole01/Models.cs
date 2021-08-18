using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectConsole01
{
  public class Blog
  {
    public int BlogId { get; set; }
    
    public string Url { get; set; }

    public List<Post> Posts { get; } = new List<Post>();
  }

  public class Post
  {
    public int PostId { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }

    public Blog Blog { get; set; }
    
    public int BlogId { get; set; }
  }

  public class BloggingContext : DbContext
  {
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; private set; }

    public BloggingContext()
    {
      var rootPath = Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData
      );

      DbPath = $"{rootPath}{System.IO.Path.DirectorySeparatorChar}blogging.db";
    }

    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder
    ) {
      optionsBuilder.UseSqlite($"Data source={DbPath}");
    }
  }
}
