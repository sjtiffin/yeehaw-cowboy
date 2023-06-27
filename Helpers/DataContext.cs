namespace yeehaw.Helpers;

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

public class DataContext {
  private DbSettings _dbSettings;

  public DataContext(IOptions<DbSettings> dbSettings) {
    _dbSettings = dbSettings.Value;
  }

  public IDbConnection CreateConnection() {
    var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};"; // TODO: add the connection shit when it's time
    return new SqlConnection(connectionString);
  }

  public async Task Init() {
    await _initDatabase();
    await _initTables();
  }

  private async Task _initDatabase() {
    var connectionString = $"Server={_dbSettings.Server}; Database=master; User Id={_dbSettings.UserId}; Password={_dbSettings.Password};";
    using var connection = new SqlConnection(connectionString);
    var sql = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dbSettings.Database}') CREATE DATABASE [{_dbSettings.Database}];";
    await connection.ExecuteAsync(sql);
  }

  private async Task _initTables() {
    using var connection = CreateConnection();
    await _initTasks();

    async Task _initTasks() {
      var sql = """
                IF OBJECT_ID('Tasks', 'T') IS NULL
                CREATE TABLE Tasks (
                    Id INT NOT NULL PRIMARY KEY IDENTITY,
                    Title NVARCHAR(100),
                    Completed BIT,
                    DueDate DATE,
                );
            """;
        await connection.ExecuteAsync(sql);
    }
  }

}