using Microsoft.EntityFrameworkCore;
using ProjectWs03.src.Models;

namespace ProjectWs03.src.Database
{
  public class DatabaseContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Product> Products { get; set; }

    public DatabaseContext()
    {   
    }

    public DatabaseContext (DbContextOptions<DatabaseContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>(
        entity => {
          entity.Property(c => c.Name)
          .IsRequired()
          .HasMaxLength(45);
        }
      );

      modelBuilder.Entity<Order>(
        entity => {
          entity.HasMany(o => o.Products)
          .WithMany(p => p.Orders)
          .UsingEntity<OrderProduct>(
            join => join.HasOne(product => product.Product)
              .WithMany(product => product.OrderProducts)
              .HasForeignKey(product => product.ProductId),
            join => join.HasOne(order => order.Order)
              .WithMany(order => order.OrderProducts)
              .HasForeignKey(order => order.OrderId),
            join =>
            {
              join.HasKey(p => new { p.OrderId, p.ProductId });
            } 
          );
        }
      );
    }
  }
}
