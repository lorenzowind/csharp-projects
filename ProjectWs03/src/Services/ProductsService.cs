using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectWs03.src.Models;
using ProjectWs03.src.Database;

namespace ProjectWs03.src.Services
{
  public class ProductsService : IProductsService
  {
    private readonly DatabaseContext _database;

    public ProductsService(DbContextOptions<DatabaseContext> dbContextOptions) 
    {
      _database = new DatabaseContext(dbContextOptions);
    }

    public IEnumerable<Product> GetAll()
    {
      return _database.Products;
    }

    public Product GetById(int id)
    {
      return _database.Products.Find(id);
    }

    public void Add(Product product)
    {
      _database.Add(product);
      _database.SaveChanges();
    }

    public void Update(Product product)
    {
      var productFound = this.GetById(product.Id);
      productFound = product;

      _database.SaveChanges();
    }

    public void Remove(Product product)
    {
      var productFound = this.GetById(product.Id);

      _database.Remove(productFound);
      _database.SaveChanges();
    }
  }
}
