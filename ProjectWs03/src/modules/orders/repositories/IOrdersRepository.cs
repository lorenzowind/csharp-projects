using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectWs03.src.modules.orders.models;

namespace ProjectWs03.src.modules.orders.repositories
{
  public interface IOrdersRepository
  {
    Task<IEnumerable<Order>> GetAllByCustomerId(int customerId);
    
    Task<Order> GetById(int id);
    
    Task Add(Order order);

    Task Update(Order order);

    Task Remove(Order order);
  }
}
