using System.ComponentModel.DataAnnotations;

namespace ProjectWs03.src.modules.sessions.models
{
  public class Session
  {
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
