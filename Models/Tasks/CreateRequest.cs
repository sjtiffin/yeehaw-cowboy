namespace yeehaw.Models.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class CreateRequest {
  [Required]
  public string? Title { get; set; }

  [Required]
  public string? Completed { get; set; }

  [Required]
  public string? DueDate { get; set; }
}