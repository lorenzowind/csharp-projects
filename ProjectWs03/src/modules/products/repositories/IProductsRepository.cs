using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectWs03.src.modules.products.models;

namespace ProjectWs03.src.modules.products.repositories
{
  public interface IProductsRepository
  {
    IEnumerable<Product> GetAll();
    
    Task<Product> GetById(int id);
    
    Task Add(Product product);

    Task Update(Product product);

    Task Remove(Product product);
  }
}
