using System;
using System.Collections.Generic;

namespace ProjectWs02.src.DTOs
{
  public class OrderDTO
  {
    public int Code { get; set; }
    
    public DateTime DoneDate { get; set; }
    
    public string CustomerName { get; set; }
    
    public string CustomerEmail { get; set; }
    
    public IEnumerable<OrderItemDTO> Items { get; set; }
    
    public decimal TotalValue { get; set; }
  }

  public class OrderItemDTO
  {
    public int ProductId {get; set;}
    
    public string ProductName {get; set;}
    
    public int Quantity { get; set; }
  }
}
