using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
  public class DbItem
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; } = "No Description";

    public string Img { get; set; } = "//placehold.it/100x100";

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Profile Creator { get; set; }


  }
}