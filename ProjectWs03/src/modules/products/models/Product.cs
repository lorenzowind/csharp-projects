using System.Collections.Generic;

using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.ordersProducts.models;

namespace ProjectWs03.src.modules.products.models
{
  public class Product
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
  }
}
