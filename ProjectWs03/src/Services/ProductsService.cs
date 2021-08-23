using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectWs03.src.Models;
using ProjectWs03.src.Database;

namespace ProjectWs03.src.Services
{
  public class ProductsService : IProductsService
  {
    private readonly DatabaseContext _database;

    public ProductsService(IServiceCollection serviceCollection) 
    {
      _database = serviceCollection
        .BuildServiceProvider()
        .GetRequiredService<DatabaseContext>();
    }

    public IEnumerable<Product> GetAll()
    {
      return _database.Products;
    }

    public async Task<Product> GetById(int id)
    {
      return await _database.Products
        .AsNoTracking()
        .SingleOrDefaultAsync(p => p.Id == id);
    }

    // public void Add(ProductDTO product)
    // {
    //   _database.Add(product);
    //   _database.SaveChanges();
    // }

    // public void Update(Product product)
    // {
    //   var productFound = this.GetById(product.Id);
    //   productFound = product;

    //   _database.SaveChanges();
    // }

    // public void Remove(Product product)
    // {
    //   var productFound = this.GetById(product.Id);

    //   _database.Remove(productFound);
    //   _database.SaveChanges();
    // }
  }
}
