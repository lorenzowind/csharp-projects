using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.modules.customers.models;
using ProjectWs03.src.shared.database.contexts;
using ProjectWs03.src.shared.database.utils;

namespace ProjectWs03.src.modules.customers.repositories
{
  public class CustomersRepository : ICustomersRepository
  {
    private readonly SqlServerContext _sqlServerDatabase;

    public CustomersRepository(SqlServerService sqlServerService) 
    {
      _sqlServerDatabase = sqlServerService.context;
    }

    public async Task<Customer> GetById(int id)
    {
      return await _sqlServerDatabase.Customers
        .AsNoTracking()
        .SingleOrDefaultAsync(customer => customer.Id == id);
    }

    public async Task Add(Customer customer)
    {
      await _sqlServerDatabase.AddAsync(customer);
      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Update(Customer customer)
    {
      var customerFound = await this.GetById(customer.Id);
      customerFound = customer;

      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Remove(Customer customer)
    {
      var customerFound = await this.GetById(customer.Id);

      _sqlServerDatabase.Remove(customerFound);
      await _sqlServerDatabase.SaveChangesAsync();
    }
  }
}
