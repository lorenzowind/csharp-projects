using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.shared.database.contexts;
using ProjectWs03.src.shared.database.utils;

namespace ProjectWs03.src.modules.orders.repositories
{
  public class OrdersRepository : IOrdersRepository
  {
    private readonly SqlServerContext _sqlServerDatabase;

    public OrdersRepository(SqlServerService sqlServerService) 
    {
      _sqlServerDatabase = sqlServerService.context;
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerId(int customerId)
    {
      return await _sqlServerDatabase.Orders
        .Include(order => order.Customer)
        .Include(order => order.OrderProducts)
        .ThenInclude(orderProduct => orderProduct.Product)
        .Where(order => order.CustomerId == customerId)
        .OrderBy(order => order.DoneDate)
        .ToListAsync();
    }

    public async Task<Order> GetById(int id)
    {
      return await _sqlServerDatabase.Orders
        .AsNoTracking()
        .SingleOrDefaultAsync(order => order.Id == id);
    }

    public async Task Add(Order order)
    {
      await _sqlServerDatabase.AddAsync(order);
      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Update(Order order)
    {
      var orderFound = await this.GetById(order.Id);
      orderFound = order;

      await _sqlServerDatabase.SaveChangesAsync();
    }

    public async Task Remove(Order order)
    {
      var orderFound = await this.GetById(order.Id);

      _sqlServerDatabase.Remove(orderFound);
      await _sqlServerDatabase.SaveChangesAsync();
    }
  }
}
