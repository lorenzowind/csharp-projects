using System;
using System.Linq;
using System.Collections.Generic;

using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.ordersProducts.models;

namespace ProjectWs03.src.modules.orders.dtos
{
  public class OrderDTO
  {
    public int Id { get; set; }
    
    public DateTime DoneDate { get; set; }
    
    public string CustomerName { get; set; }
    
    public string CustomerEmail { get; set; }
    
    public IEnumerable<OrderItemDTO> Items { get; set; }
    
    public decimal TotalValue { get; set; }

    public static OrderDTO FromOrder(Order order)
    {
      return new OrderDTO
      {
        Id = order.Id,
        DoneDate = order.DoneDate,
        CustomerName = order.Customer.Name,
        CustomerEmail = order.Customer.Email,
        Items = order.OrderProducts.Select(
          orderProduct => OrderItemDTO.FromOrderItem(orderProduct)
        ),
        TotalValue = order.OrderProducts.Sum(
          orderProduct => orderProduct.UnitaryValue * orderProduct.Quantity
        )
      };
    }
  }

  public class OrderItemDTO
  {
    public int ProductId {get; set;}
    
    public string ProductName {get; set;}
    
    public int Quantity { get; set; }
    
    public static OrderItemDTO FromOrderItem(OrderProduct orderProduct)
    {
      return new OrderItemDTO
      {
        ProductId = orderProduct.Product.Id,
        ProductName = orderProduct.Product.Name,
        Quantity = orderProduct.Quantity
      };
    }
  }
}
