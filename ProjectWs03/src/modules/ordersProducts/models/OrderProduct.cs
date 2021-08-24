using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.products.models;

namespace ProjectWs03.src.modules.ordersProducts.models
{
  public class OrderProduct
  {
    public int Quantity { get; set; }

    public decimal UnitaryValue { get; set; }
    
    public Order Order { get; set; }

    public int OrderId { get; set; }
    
    public Product Product { get; set; }

    public int ProductId { get; set; }
  }
}
