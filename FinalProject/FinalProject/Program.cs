using DatabaseConnector;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            Database db = null;
            string connection = "";
            if (File.Exists("dbcstring.json"))
            {
                using (var fs = new FileStream("dbcstring.json",FileMode.Open))
                {
                    db = JsonSerializer.Deserialize<Database>(fs)!;
                }
            }

            switch (db.Name)
            {
                case "Postgre":
                    connection = db.ConnectionString;
                    builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));
                    break;
                case "MySQL":
                    connection = db.ConnectionString;
                    builder.Services.AddDbContext<Context>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 11))));
                    break;
                case "MSSQL":
                    connection = db.ConnectionString;
                    builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));
                    break;
                default:
                    break;
            }







            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}