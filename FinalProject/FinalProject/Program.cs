using DatabaseConnector;
using DatabaseConnector.Interfaces;
//using DatabaseConnector.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using FinalProject.DataBaseContext;

namespace FinalProject;

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
/*
{
  "Name": "Postgre",
  "UserName": "Ivan",
  "ConnectionString": "Host=localhost;Port=5432;Database=TheForumDB;Username=postgres;Password=393318156a"
}
{
  "Name": "MySQL",
  "UserName": "Ivan",
  "ConnectionString": "Server=localhost;Port=3306;Database=FinalProjectDatabase;Uid=bzic;Pwd=393318156a404056792b;"
}
*/

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
        }

        switch (db.Name)
        {
            case "Postgre":
                connection = db.ConnectionString;
                builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));
                break;
            case "MySQL":
                connection = db.ConnectionString;
                builder.Services.AddDbContext<Context>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 28))));
                break;

            case "MSSQL":
                connection = db.ConnectionString;
                builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));
                break;

            default:
                break;
        }

        builder.Services.AddScoped<EFGenericRepository<User>>();
        builder.Services.AddScoped<EFGenericRepository<Comment>>();
        builder.Services.AddScoped<EFGenericRepository<Post>>();
        builder.Services.AddScoped<EFGenericRepository<Issue>>();
        builder.Services.AddScoped<EFGenericRepository<SessionInfo>>();

        

        var app = builder.Build();

        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapGet("/users", async (Context db) => await db.Users.ToListAsync());
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        
        app.Run();
    }
}
