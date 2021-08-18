using System.ComponentModel.DataAnnotations;

namespace ProjectWs01.src.Models
{
  public class ZipCodeQuery 
  {
    [Required]
    [StringLength(
      255, 
      ErrorMessage = "PublicArea must contains maximum 255 characters"
    )]
    public string PublicArea { get; set; }
    
    [Required]
    [StringLength(
      255, 
      ErrorMessage = "District must contains maximum 255 characters"
    )]
    public string District { get; set; }
    
    [Required]
    [StringLength(
      255, 
      ErrorMessage = "City must contains maximum 255 characters"
    )]
    public string City { get; set; }
    
    [Required]
    [RegularExpression(
      @"^[A-Z]{2}$", 
      ErrorMessage = "State must contains 2 characters"
    )]
    public string State { get; set; }
    
    [Required]
    [RegularExpression(
      @"^\d{8}$", 
      ErrorMessage = "ZipCode must contains 8 digits"
    )]
    public string ZipCode { get; set; }

    public override string ToString()
    {
      return $"[PublicArea: {PublicArea}, " + 
        $"District: {District}, " + 
        $"City: {City}, " + 
        $"State: {State}, " + 
        $"ZipCode: {ZipCode}]";
    }
  }
}
