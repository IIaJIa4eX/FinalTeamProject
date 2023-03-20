using DatabaseConnector;
using FinalProject.DataBaseContext;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Text.Json;

namespace FinalProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        string connection = "";
        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
            logging.RequestHeaders.Add("Authorization");
            logging.RequestHeaders.Add("X-Real-IP");
            logging.RequestHeaders.Add("X-Forwared-For");
        });
        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });


        // builder.Services.AddControllersWithViews();


        if (File.Exists("dbcstring.json"))
        {
            using (var fs = new FileStream("dbcstring.json", FileMode.Open))
            {
                connection = JsonSerializer.Deserialize<Database>(fs)!.ConnectionString;
                builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));
            }
        }
        else
        {
            throw new FileLoadException("dbcstring.json not exist, no connection string for database!");
        }

        builder.Services.AddScoped<EFGenericRepository<User>>();
        builder.Services.AddScoped<EFGenericRepository<Comment>>();
        builder.Services.AddScoped<EFGenericRepository<Post>>();
        builder.Services.AddScoped<EFGenericRepository<Issue>>();
        builder.Services.AddScoped<EFGenericRepository<SessionInfo>>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //app.UseExceptionHandler("/Home/Error");
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseHttpLogging();
        app.MapGet("/users", async (Context db) => await db.Users.ToListAsync());
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
