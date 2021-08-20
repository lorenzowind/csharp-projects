using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWs03.src.DTOs;
using ProjectWs03.src.Models;
using ProjectWs03.src.Services;

namespace ProjectWs03.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
      private readonly ILogger<ProductsController> _logger;
      private readonly IProductsService _productsService;

      public ProductsController(
        ILogger<ProductsController> logger,
        IProductsService productsService
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
      public ActionResult<ProductDTO> GetProductById(int id)
      {
        var product = _productsService.GetById(id);

        if (product == null)
        {
          return NotFound();          
        }

        return ProductDTO.FromProduct(product);
      }

      // POST /products
      [HttpPost]
      public ActionResult<ProductDTO> AddProduct(Product product)
      {
        var auxProduct = _productsService.GetById(product.Id);

        if (auxProduct == null)
        {
          return NotFound();          
        }

        _productsService.Add(product);

        return CreatedAtAction(
          nameof(GetProductById),
          new { id = product.Id },
          product
        );
      }

      // PUT /products
      [HttpPut]
      public ActionResult<ProductDTO> UpdateProduct(Product product)
      {
        var auxProduct = _productsService.GetById(product.Id);

        if (auxProduct == null)
        {
          return NotFound();          
        }

        _productsService.Update(product);

        return AcceptedAtAction(
          nameof(GetProductById),
          new { id = product.Id },
          product
        );
      }

      // DELETE /products/:id
      [HttpDelete("{id}")]
      public ActionResult<ProductDTO> RemoveProduct(int id)
      {
        var auxProduct = _productsService.GetById(id);

        if (auxProduct == null)
        {
          return NotFound();          
        }

        _productsService.Remove(auxProduct);

        return Accepted();
      }
    }
}
