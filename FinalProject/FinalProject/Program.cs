using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text.Json;

namespace FinalProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Database db = null;
        string connection = "";
        if (File.Exists("dbcstring.json"))
        {

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


            using (var fs = new FileStream("dbcstring.json", FileMode.Open))
            {

                db = JsonSerializer.Deserialize<Database>(fs)!;
                {
                }


            }
        }


        connection = db.ConnectionString;
        builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));


        builder.Services.AddScoped<IUserRepository, UserRepository>();


        builder.Services.AddScoped<EFGenericRepository<Comment>>();
        builder.Services.AddScoped<EFGenericRepository<Post>>();
        builder.Services.AddScoped<EFGenericRepository<Issue>>();
        builder.Services.AddScoped<EFGenericRepository<SessionInfo>>();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProject", Version = "v1" });
        });

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
        app.MapControllers();
        app.MapGet("/users", async (Context db) => await db.Users.ToListAsync());
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
