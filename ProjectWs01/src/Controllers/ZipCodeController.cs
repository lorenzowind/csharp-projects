using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWs01.src.Models;
using ProjectWs01.src.Services;

namespace ProjectWs01.src.Controllers
{
  [ApiController]
  [Route("[controller]")] 
  // [controller] takes the text before Controller label in class name
  public class ZipCodeController : ControllerBase
  {
    private readonly ILogger<ZipCodeController> _logger;
    private readonly IZipCodeService _zipCodeService;

    public ZipCodeController(
      ILogger<ZipCodeController> logger, 
      IZipCodeService zipCodeService
    ) {
        _logger = logger;
        _zipCodeService = zipCodeService;
    }

    // GET /zipcode/{:zipCode}
    [HttpGet("{zipCode}")]
    public ActionResult<ZipCodeQuery> GetZipCodeQuery(string zipCode)
    {
      _logger.LogInformation($"GetZipCodeQuery: {zipCode}");
      
      var zipCodeQuery = _zipCodeService.GetByZipCode(zipCode);

      if (zipCodeQuery == null) {
        return NotFound();
      }

      return zipCodeQuery;
    }

    // GET /zipcode
    [HttpGet]
    public IEnumerable<ZipCodeQuery> GetAllZipCodeQuery()
    {
      _logger.LogInformation("GetAllZipCodeQuery");
      
      return _zipCodeService.GetAll();
    }
  }
}
