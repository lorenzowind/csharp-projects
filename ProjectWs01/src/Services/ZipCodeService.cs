using System.Collections.Concurrent;
using System.Collections.Generic;
using ProjectWs01.src.Models;

namespace ProjectWs01.src.Services
{
  public class ZipCodeService : IZipCodeService
  {
    private readonly ConcurrentDictionary<string, ZipCodeQuery> _data = 
      new ConcurrentDictionary<string, ZipCodeQuery>();

    public ZipCodeService()
    {
      _data.TryAdd("1", new ZipCodeQuery {
        City = "Test 1",
        District = "Test 1",
        PublicArea = "Test 1",
        State = "Test 1",
        ZipCode = "1",
      });

      _data.TryAdd("2", new ZipCodeQuery {
        City = "Test 2",
        District = "Test 2",
        PublicArea = "Test 2",
        State = "Test 2",
        ZipCode = "2",
      });
    }

    public ZipCodeQuery GetByZipCode(string zipCode) 
    {
      _data.TryGetValue(zipCode, out ZipCodeQuery zipCodeQuery);
      
      return zipCodeQuery;
    }
    
    public IEnumerable<ZipCodeQuery> GetAll()
    {
      return _data.Values;
    }
  }
}
