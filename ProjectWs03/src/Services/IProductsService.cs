using System.Collections.Generic;
using ProjectWs03.src.Models;

namespace ProjectWs03.src.Services
{
  public interface IProductsService
  {
    IEnumerable<Product> GetAll();
    
    Product GetById(int id);
    
    void Add(Product product);

    void Update(Product product);

    void Remove(Product product);
  }
}
