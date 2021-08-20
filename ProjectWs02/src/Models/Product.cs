using System.Collections.Generic;

namespace ProjectWs02.src.Models
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
