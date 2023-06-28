namespace yeehaw.Models.Tasks;

public class UpdateRequest {
  public string? Title { get; set; }
  public string? Completed { get; set; }
  public string? DueDate { get; set; }
}