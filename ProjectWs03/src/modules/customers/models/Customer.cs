using System.Collections.Generic;

using ProjectWs03.src.modules.orders.models;

namespace ProjectWs03.src.modules.customers.models
{
  public class Customer
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Email { get; set; }

    public List<Order> Orders { get; set; }
  }
}
