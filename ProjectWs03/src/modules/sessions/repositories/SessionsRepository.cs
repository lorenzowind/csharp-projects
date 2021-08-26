using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ProjectWs03.src.modules.sessions.repositories
{
  public class SessionsRepository : ISessionsRepository
  {
    public string generateJWT()
    {
      var secretKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes("22e59f9f9052eebb28b09e45e34105cb")
      );  
      
      var signinCredentials = new SigningCredentials(
        secretKey, SecurityAlgorithms.HmacSha256
      );  

      var tokeOptions = new JwtSecurityToken(  
        issuer: "https://localhost:5001",  
        audience: "https://localhost:5001",  
        claims: new List<Claim>(),  
        expires: DateTime.Now.AddMinutes(1440),  
        signingCredentials: signinCredentials  
      );  

      return new JwtSecurityTokenHandler().WriteToken(tokeOptions);  
    }
  }
}
