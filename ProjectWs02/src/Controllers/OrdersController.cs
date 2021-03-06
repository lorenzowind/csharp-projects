using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjectWs02.src.DTOs;
using ProjectWs02.src.Models;
using ProjectWs02.src.Database;

namespace ProjectWs02.src.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ILogger<OrdersController> _logger;
    private readonly DatabaseContext _database;

    public OrdersController(
      ILogger<OrdersController> logger,
      DatabaseContext database
    ) {
      _logger = logger;
      _database = database;
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
        
        var customer = await _database.Customers.FindAsync(cart.CustomerId);
        
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
          var product = await _database.Products.FindAsync(item.ProductId);

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

        await _database.Orders.AddAsync(order);
        await _database.SaveChangesAsync();

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
        var customer = await _database.Customers.FindAsync(customerId);

        if (customer == null)
        {
          return BadRequest("Customer not found");
        }

        // Eager loading
        var orders = await _database.Orders
          .Include(order => order.Customer)
          .Include(order => order.OrderProducts)
          .ThenInclude(orderProduct => orderProduct.Product)
          .Where(order => order.CustomerId == customerId)
          .OrderBy(order => order.DoneDate)
          .ToListAsync();
        
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
