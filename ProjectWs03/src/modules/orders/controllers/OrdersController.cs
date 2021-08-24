using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProjectWs03.src.modules.customers.repositories;
using ProjectWs03.src.modules.products.repositories;
using ProjectWs03.src.modules.orders.repositories;
using ProjectWs03.src.modules.orders.models;
using ProjectWs03.src.modules.orders.dtos;
using ProjectWs03.src.modules.ordersProducts.models;
using ProjectWs03.src.shared.dtos;

namespace ProjectWs03.src.modules.orders.controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ILogger<OrdersController> _logger;
    private readonly ICustomersRepository _customersService;
    private readonly IOrdersRepository _ordersService;
    private readonly IProductsRepository _productsService;

    public OrdersController(
      ILogger<OrdersController> logger,
      ICustomersRepository customersService,
      IOrdersRepository ordersService,
      IProductsRepository productsService
    ) {
      _logger = logger;
      _customersService = customersService;
      _ordersService = ordersService;
      _productsService = productsService;
    }

    //POST /orders
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<OrderDTO>> ProcessCart(CartDTO cart)
    {
      try
      {
        var order = new Order();

        order.DoneDate = DateTime.Now;
        
        var customer = await _customersService.GetById(cart.CustomerId);
        
        if (customer == null)
        {
          return BadRequest("Customer not found");
        }
        
        order.Customer = customer;
        
        if (cart.Items.Count() == 0)
        {
          return BadRequest("Empty cart");
        }

        order.OrderProducts = new List<OrderProduct>();
        decimal totalValue = 0;
        
        foreach (var item in cart.Items)
        {
          var product = await _productsService.GetById(item.ProductId);

          if (product == null)
          {
            return BadRequest($"Product with id {item.ProductId} not found");
          }
        
          var orderItem = new OrderProduct();
          
          orderItem.Product = product;
          orderItem.Quantity = item.Quantity;
          orderItem.UnitaryValue = product.Price;

          order.OrderProducts.Add(orderItem);

          totalValue += product.Price * item.Quantity;
        }

        await _ordersService.Add(order);

        var result = new OrderDTO();

        result.Id = order.Id;
        result.DoneDate = order.DoneDate;
        result.CustomerName = order.Customer.Name;
        result.CustomerEmail = order.Customer.Email;
        result.Items = 
          order.OrderProducts.Select(orderProduct => new OrderItemDTO {
            ProductId = orderProduct.Product.Id,
            ProductName = orderProduct.Product.Name,
            Quantity = orderProduct.Quantity
          }).ToList();
        result.TotalValue = totalValue;
        
        return result;
      }
      catch (Exception error)
      {
        _logger.LogError(error, $"An error ocurred to process cart");
        
        throw;
      }
    }
    
    //GET /orders?customerId={:id}
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<OrderDTO>>> GetOrdersByCustomerId(
      int customerId
    ) {
      try
      {
        var customer = await _customersService.GetById(customerId);

        if (customer == null)
        {
          return BadRequest("Customer not found");
        }

        var orders = await _ordersService.GetAllByCustomerId(customer.Id);
        
        var result = orders.Select(order => OrderDTO.FromOrder(order)).ToList();
        
        return result;
      } 
      catch (Exception error)
      {
        _logger.LogError(
          error, 
          $"An error ocurred to get order by customer with id {customerId}"
        );
        
        throw;
      }
    }
  }
}
