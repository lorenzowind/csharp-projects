using System.Threading.Tasks;

using ProjectWs03.src.modules.customers.models;

namespace ProjectWs03.src.modules.customers.repositories
{
  public interface ICustomersRepository
  { 
    Task<Customer> GetById(int id);

    Task<Customer> GetByEmail(string email);
    
    Task Add(Customer customer);

    Task Update(Customer customer);

    Task Remove(Customer customer);
  }
}
