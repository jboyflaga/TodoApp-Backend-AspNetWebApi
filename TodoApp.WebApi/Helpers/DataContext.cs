namespace TodoApp.WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using TodoApp.WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    #region Ignore
    public DbSet<SampleDataFromDb> SampleDataFromDb { get; set; } = null!;
    #endregion
}