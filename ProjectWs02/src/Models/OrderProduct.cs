namespace ProjectWs02.src.Models
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
