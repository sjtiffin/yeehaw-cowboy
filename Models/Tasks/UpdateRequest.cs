namespace yeehaw.Models.Tasks;

using System.ComponentModel.DataAnnotations;
using yeehaw.Entities;

public class UpdateRequest {
  public string? Title { get; set; }
  public bool? Completed { get; set; }
  public DateTime? DueDate { get; set; }
}