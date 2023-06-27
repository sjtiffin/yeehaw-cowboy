namespace yeehaw.Services;

using AutoMapper;
using yeehaw.Entities;
using yeehaw.Helpers;
using yeehaw.Models.Tasks;
using yeehaw.Repositories;

public interface ITaskService {
  IEnumerable<Task> GetTasks();
  Task GetTask(int id);
  void CreateTask(CreateRequest model);
  void UpdateTask(int id, UpdateRequest model);
  void DeleteTask(int id);
}

public class TaskService : ITaskService {
  private ITaskRepository _taskRepository;
  private readonly IMapper _mapper;

  public TaskService(ITaskRepository taskRepository, IMapper mapper) {
    _taskRepository = taskRepository;
    _mapper = mapper;
  }

  public IEnumerable<Task> GetTasks() {
    return _taskRepository.GetTasks();
  }

  public Task GetTask(int id) {

    try {
      var task = _taskRepository.GetTask(id);
      return task;
    } catch (Exception ex) {
      throw new AppException("Task not found", ex);
    }
  }  

  public void CreateTask(CreateRequest model) {
    var task = _mapper.Map<Task>(model);

    try {
      _taskRepository.CreateTask(task);
    } catch (Exception e) {
      throw new AppException("Task could not be created", e);
    }
  }

  public void UpdateTask(int id, UpdateRequest model) {
    var task = _taskRepository.GetTask(id);

    if (task == null) throw new AppException("Task not found");

    _mapper.Map(model, task);

    try {
      _taskRepository.UpdateTask(task);
    } catch (Exception e) {
      throw new AppException("Task could not be updated", e);
    }
  }

  public void DeleteTask(int id) {
    try {
      _taskRepository.DeleteTask(id);
    } catch (Exception e) {
      throw new AppException("Task could not be deleted", e);
    }
  }

}