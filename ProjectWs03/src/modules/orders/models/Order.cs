using System;
using System.Collections.Generic;

using ProjectWs03.src.modules.products.models;
using ProjectWs03.src.modules.ordersProducts.models;
using ProjectWs03.src.modules.customers.models;

namespace ProjectWs03.src.modules.orders.models
{
  public class Order
  {
    public int Id { get; set; }
    
    public DateTime DoneDate { get; set; }

    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public ICollection<Product> Products { get; set; }

    public List<OrderProduct> OrderProducts { get; set; }
  }
}
