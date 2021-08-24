using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProjectWs03.src.modules.products.dtos;
using ProjectWs03.src.modules.products.repositories;

namespace ProjectWs03.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
      private readonly ILogger<ProductsController> _logger;
      private readonly IProductsRepository _productsService;

      public ProductsController(
        ILogger<ProductsController> logger,
        IProductsRepository productsService
      ) {
        _logger = logger;
        _productsService = productsService;
      }

      // GET /products
      [HttpGet]
      public IEnumerable<ProductDTO> GetAllProducts()
      {
        return _productsService.GetAll().Select(ProductDTO.FromProduct);
      }

      // GET /products/{:id}
      [HttpGet("{id}")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      [ProducesResponseType(500)]
      public async Task<ActionResult<ProductDTO>> GetProductById(int id)
      {
        try
        {
          var product = await _productsService.GetById(id);

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
            $"An error occurred to find product with id {id}"
          );

          throw;
        }
      }

      // // POST /products
      // [HttpPost]
      // public ActionResult<ProductDTO> AddProduct(ProductDTO product)
      // {
      //   var auxProduct = _productsService.GetById(product.Id);

      //   if (auxProduct != null)
      //   {
      //     return Unauthorized();          
      //   }

      //   _productsService.Add(product);

      //   return CreatedAtAction(
      //     nameof(GetProductById),
      //     new { id = product.Id },
      //     product
      //   );
      // }

      // // PUT /products
      // [HttpPut]
      // public ActionResult<ProductDTO> UpdateProduct(Product product)
      // {
      //   var auxProduct = _productsService.GetById(product.Id);

      //   if (auxProduct == null)
      //   {
      //     return NotFound();          
      //   }

      //   _productsService.Update(product);

      //   return AcceptedAtAction(
      //     nameof(GetProductById),
      //     new { id = product.Id },
      //     product
      //   );
      // }

      // // DELETE /products/:id
      // [HttpDelete("{id}")]
      // public ActionResult<ProductDTO> RemoveProduct(int id)
      // {
      //   var auxProduct = _productsService.GetById(id);

      //   if (auxProduct == null)
      //   {
      //     return NotFound();          
      //   }

      //   _productsService.Remove(auxProduct);

      //   return Accepted();
      // }
    }
}
