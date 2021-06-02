namespace Server.Models
{
  public class Keep : DbItem
  {
    public int Views { get; set; } = 0;
    public int Shares { get; set; } = 0;
    public int Keeps { get; set; } = 0;

  }
}