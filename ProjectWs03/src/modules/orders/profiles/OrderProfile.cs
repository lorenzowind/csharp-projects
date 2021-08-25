using AutoMapper;

using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.orders.dtos;
using ProjectWs03.src.modules.ordersProducts.models;

namespace ProjectWs03.src.modules.orders.profiles
{
  public class OrderProfile: Profile
  {
    public OrderProfile()
    {
      CreateMap<Order, OrderDTO>();

      CreateMap<OrderProduct, OrderItemDTO>();
    }
  }
}
