using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.WebApi.Helpers;

namespace TodoApp.DbInit.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            //string connectionString = configuration.GetConnectionString("WebApiDatabase");

            //DbContextOptionsBuilder<DataContext> optionsBuilder = new DbContextOptionsBuilder<DataContext>()
            //    .UseSqlServer(connectionString);

            using (DataContext sc = new DataContext(configuration))
            {

                sc.Database.Migrate();

                //sc.Students.AddRange
                //(
                //    new Student { Name = "Isaac Newton" },
                //    new Student { Name = "C.F. Gauss" },
                //    new Student { Name = "Albert Einstein" }
                //);

                //sc.SaveChanges();
            }
        }
    }
}