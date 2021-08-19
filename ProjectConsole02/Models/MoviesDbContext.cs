using System;
using Microsoft.EntityFrameworkCore;

namespace ProjectConsole02.Models
{
  public class MoviesDbContext : DbContext
  {
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Movie>(
        entity => {
          entity.Property(m => m.Title)
            .IsRequired();
        }
      );
    }

    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder
    ) {
      optionsBuilder.UseSqlServer(
        @"Data Source=W10J6VSQ93;" + 
        "Initial Catalog=ProjectConsole02;" +
        "Integrated Security=True"
      );

      optionsBuilder.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
    }
  }
}
