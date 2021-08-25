using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.modules.products.models;
using ProjectWs03.src.shared.database.contexts;
using ProjectWs03.src.shared.database.utils;

namespace ProjectWs03.src.modules.products.repositories
{
  public class ProductsRepository : IProductsRepository
  {
    private readonly SqlServerContext _sqlServerDatabase;

    public ProductsRepository(SqlServerService sqlServerService) 
    {
      _sqlServerDatabase = sqlServerService.context;
    }

    public IEnumerable<Product> GetAll()
    {
      return _sqlServerDatabase.Products;
    }

    public async Task<Product> GetById(int id)
    {
      return await _sqlServerDatabase.Products
        .AsNoTracking()
        .SingleOrDefaultAsync(product => product.Id == id);
    }

    public async Task Add(Product product)
    {
      await _sqlServerDatabase.AddAsync(product);
      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
      var productFound = await this.GetById(product.Id);
      productFound = product;

      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Remove(Product product)
    {
      var productFound = await this.GetById(product.Id);

      _sqlServerDatabase.Remove(productFound);
      await _sqlServerDatabase.SaveChangesAsync();
    }
  }
}
