using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.modules.customers.models;
using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.ordersProducts.models;
using ProjectWs03.src.modules.products.models;

namespace ProjectWs03.src.shared.database.contexts
{
  public class SqlServerContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Product> Products { get; set; }

    public SqlServerContext()
    {   
    }

    public SqlServerContext (DbContextOptions<SqlServerContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>(
        entity => {
          entity.Property(customer => customer.Name)
          .IsRequired()
          .HasMaxLength(45);
        }
      );

      modelBuilder.Entity<Order>(
        entity => {
          entity.HasMany(order => order.Products)
          .WithMany(product => product.Orders)
          .UsingEntity<OrderProduct>(
            join => join.HasOne(product => product.Product)
              .WithMany(product => product.OrderProducts)
              .HasForeignKey(product => product.ProductId),
            join => join.HasOne(order => order.Order)
              .WithMany(order => order.OrderProducts)
              .HasForeignKey(order => order.OrderId),
            join =>
            {
              join.HasKey(orderProduct => new { 
                orderProduct.OrderId, orderProduct.ProductId 
              });
            } 
          );
        }
      );

      modelBuilder.Entity<OrderProduct>().ToTable("OrdersProducts");
    }
  }
}
