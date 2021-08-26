using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProjectWs03.src.modules.customers.repositories;

namespace ProjectWs03.src.modules.customers.controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CustomersController : ControllerBase
  {
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomersRepository _customersService;

    public CustomersController(
      ILogger<CustomersController> logger,
      ICustomersRepository customersService
    ) {
      _logger = logger;
      _customersService = customersService;
    }
  }
}
