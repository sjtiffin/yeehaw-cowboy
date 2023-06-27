namespace yeehaw.Models.Tasks;

using System.ComponentModel.DataAnnotations;
using yeehaw.Entities;

public class CreateRequest {
  [Required]
  public string? Title { get; set; }

  [Required]
  public bool? Completed { get; set; }

  [Required]
  public DateTime? DueDate { get; set; }
}