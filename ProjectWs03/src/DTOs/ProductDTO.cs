using ProjectWs03.src.Models;

namespace ProjectWs03.src.DTOs
{
  public class ProductDTO
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal UnitaryPrice { get; set; }

    public static ProductDTO FromProduct(Product product)
    {
      return new ProductDTO
      {
        Id = product.Id,
        Name = product.Name,
        UnitaryPrice = product.Price
      };
    }
  }
}
