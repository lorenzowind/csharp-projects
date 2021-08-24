using ProjectWs03.src.modules.products.models;

namespace ProjectWs03.src.modules.products.dtos
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
