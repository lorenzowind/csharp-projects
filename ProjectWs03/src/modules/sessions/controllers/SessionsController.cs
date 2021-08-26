using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ProjectWs03.src.modules.sessions.models;
using ProjectWs03.src.modules.sessions.repositories;
using ProjectWs03.src.modules.customers.repositories;

namespace ProjectWs03.src.modules.sessions.controllers
{
  [ApiController]
  [Route("[controller]")]
  public class SessionsController : ControllerBase
  {
    private readonly ILogger<SessionsController> _logger;
    private readonly ISessionsRepository _sessionsService;
    private readonly ICustomersRepository _customersService;

    public SessionsController(
      ILogger<SessionsController> logger,
      ISessionsRepository sessionsService,
      ICustomersRepository customersService
    ) {
      _logger = logger;
      _sessionsService = sessionsService;
      _customersService = customersService;
    }

    // POST /sessions/signIn
    [HttpPost("SignIn")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> signIn([FromBody] Session session)
    {
      try
      {
        var customer = await _customersService.GetByEmail(session.Email);

        if (customer == null)
        {
          return BadRequest("Customer not found");
        }

        if (customer.Password == session.Password)
        {
          return Ok(new { 
            Token = _sessionsService.generateJWT() 
          });
        }

        return Unauthorized();
      }
      catch (Exception error)
      {
        _logger.LogError(
          error, 
          "An error occurred to sign in the customer"
        );

        throw;
      }
    }
  }
}
