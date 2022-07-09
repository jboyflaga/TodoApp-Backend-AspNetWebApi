namespace TodoApp.WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using TodoApp.WebApi.Entities;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    #region Ignore
    public DbSet<SampleDataFromDb> SampleDataFromDb { get; set; } = null!;
    #endregion

}