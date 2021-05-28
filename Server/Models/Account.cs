using System;

namespace Server.Models
{
  public class Account : Profile
  {
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

  }
}