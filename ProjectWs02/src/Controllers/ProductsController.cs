using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWs02.src.Dtos;
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
    }
}
