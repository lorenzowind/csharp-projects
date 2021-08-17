using System.Collections.Generic;
using ProjectWs01.src.Models;

namespace ProjectWs01.src.Services
{
  public interface IZipCodeService
  {
    IEnumerable<ZipCodeQuery> GetAll();
    ZipCodeQuery GetByZipCode(string zipCode);
  }
}
