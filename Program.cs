using System.Text.Json.Serialization;
using yeehaw.Helpers;
using yeehaw.Repositories;

var builder = WebApplication.CreateBuilder(args);

{
  var services = builder.Services;
  var env = builder.Environment;

  services.AddCors();
  services.AddControllers().AddJsonOptions(x => {
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });
  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

  services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

  services.AddSingleton<DataContext>();
  services.AddScoped<ITaskRepository, TaskRepository>();
}

var app = builder.Build();
{
  using var scope = app.Services.CreateScope();
  var context = scope.ServiceProvider.GetRequiredService<DataContext>();
  await context.Init();
}

{
  app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
  );

  app.UseMiddleware<ErrorHandlerMiddleware>();
  app.MapControllers();
}

app.Run("http://localhost:5000");