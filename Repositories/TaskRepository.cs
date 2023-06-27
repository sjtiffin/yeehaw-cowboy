namespace yeehaw.Repositories;

using Dapper;
using yeehaw.Entities;
using yeehaw.Helpers;

public interface ITaskRepository {
  IEnumerable<Task> GetTasks();
  Task GetTask(int id);
  void CreateTask(Task task);
  void UpdateTask(Task task);
  void DeleteTask(int id);
}

public class TaskRepository : ITaskRepository {

  private DataContext _context;

  public TaskRepository(DataContext context) {
    _context = context;
  }

  public IEnumerable<Task> GetTasks() {
    using var connection = _context.CreateConnection();
    connection.Open();

    var tasks = connection.Query<Task>("SELECT * FROM Tasks");

    return tasks;
  }

  public Task GetTask(int id) {
    using var connection = _context.CreateConnection();
    connection.Open();

    var task = connection.QueryFirstOrDefault<Task>("SELECT * FROM Tasks WHERE Id = @Id", new { Id = id });

    return task;
  }

  public void CreateTask(Task task) {
    using var connection = _context.CreateConnection();
    connection.Open();

    var result = connection.Execute("INSERT INTO Tasks (Title, Completed, DueDate) VALUES (@Title, @Completed, @DueDate)", task);
  }

  public void UpdateTask(Task task) {
    using var connection = _context.CreateConnection();
    connection.Open();

    var result = connection.Execute("UPDATE Tasks SET Title = @Title, Completed = @Completed, DueDate = @DueDate WHERE Id = @Id", task);
  }

  public void DeleteTask(int id) {
    using var connection = _context.CreateConnection();
    connection.Open();

    var result = connection.Execute("DELETE FROM Tasks WHERE Id = @Id", new { Id = id });
  }
}