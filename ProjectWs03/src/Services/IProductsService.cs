using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWs03.src.Models;

namespace ProjectWs03.src.Services
{
  public interface IProductsService
  {
    IEnumerable<Product> GetAll();
    
    Task<Product> GetById(int id);
    
    // void Add(ProductDTO product);

    // void Update(Product product);

    // void Remove(Product product);
  }
}
