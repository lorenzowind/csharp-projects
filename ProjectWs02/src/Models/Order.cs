using System;
using System.Collections.Generic;

namespace ProjectWs02.src.Models
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
