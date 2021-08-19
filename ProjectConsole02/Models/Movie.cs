namespace ProjectConsole02.Models
{
  public class Movie
  {
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int LaunchYear { get; set; }
    
    public string Director { get; set; }
    
    public string Synopsis { get; set; }
    
    public int? Duration { get; set; }
  }
}
