namespace yeehaw.Controllers;

using Microsoft.AspNetCore.Mvc;
using yeehaw.Models.Tasks;
using yeehaw.Services;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase {
  private ITaskService _taskService;

  public TasksController(ITaskService taskService) {
    _taskService = taskService;
  }

  [HttpGet]
  public ActionResult GetTasks() {
    var tasks =  _taskService.GetTasks();
    return Ok(tasks);
  }

  [HttpGet("{id}")]
  public ActionResult GetTask(int id) {
    var task = _taskService.GetTask(id);
    return Ok(task);
  }

  [HttpPost]
  public ActionResult CreateTask(CreateRequest model) {
    _taskService.CreateTask(model);
    return Ok(new { message = "Task created successfully" });
  }

  [HttpPut("{id}")]
  public ActionResult UpdateTask(int id, UpdateRequest model) {
    _taskService.UpdateTask(id, model);
    return Ok(new { message = "Task updated successfully" });
  }

  [HttpDelete("{id}")]
  public ActionResult DeleteTask(int id) {
    _taskService.DeleteTask(id);
    return Ok(new { message = "Task deleted successfully" });
  }
}

