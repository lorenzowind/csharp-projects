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
        
        foreach (var item in cart.Items)
        {
          var product = await _database.Products.FindAsync(item.ProductId);

          if (product == null)
          {
              return BadRequest($"Product with id {item.ProductId} not found");
          }
        }

        await _database.Orders.AddAsync(order);
        
        await _database.SaveChangesAsync();   
      }
      catch (Exception error)
      {
        _logger.LogError(error, $"An error ocurred to process cart");
        
        throw;
      }
    }
  }
}
