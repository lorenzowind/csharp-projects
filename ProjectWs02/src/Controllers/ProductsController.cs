using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjectWs02.src.DTOs;
using ProjectWs02.src.Database;

namespace ProjectWs02.src.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ILogger<ProductsController> _logger;
    private readonly DatabaseContext _database;

    public ProductsController(
      ILogger<ProductsController> logger,
      DatabaseContext database
    ) {
      _logger = logger;
      _database = database;
    }

    // GET /products
    [HttpGet]
    public IEnumerable<ProductDTO> GetAllProducts()
    {
      return _database.Products.Select(ProductDTO.FromProduct);
    }

    //GET /produtos/{:id}
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
      try
      {
        var product = await _database.Products
          .AsNoTracking()
          .SingleOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
          return NotFound();
        }

        return ProductDTO.FromProduct(product);
      }
      catch (Exception error)
      {
        _logger.LogError(
          error, 
          $"An error ocurred to find product with id {id}"
        );
        
        throw;
      }
    }
  }
}
