using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectWs02.src.DTOs
{
  public class CartDTO
  {
    [Required]
    public int CustomerId { get; set; }
    
    [Required]
    [MaxLength(45)]
    public string CustomerName { get; set; }
    
    [EmailAddress]
    public string CustomerEmail { get; set; }
    
    public IEnumerable<CartItemDTO> Items { get; set; }
  }

  public class CartItemDTO
  {
    [Required]
    public int ProductId {get; set;}
    
    [Required]
    [Range(1, 10)]
    public int Quantity { get; set; }
  }
}
