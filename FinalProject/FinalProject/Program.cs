using DatabaseConnector;
using FinalProject.BusinessLogicLayer;
using FinalProject.DataBaseContext;
using FinalProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;
using System.Text.Json;

namespace FinalProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
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

        if (File.Exists("dbcstring.json"))
        {
            using (var fs = new FileStream("dbcstring.json", FileMode.Open))
            {
                string connection = JsonSerializer.Deserialize<Database>(fs)!.ConnectionString;
                builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));
            }
        }
        else
        {
            throw new FileLoadException("dbcstring not exist, can`t find connection string for database!");
        }


        builder.Services.AddControllers();
        builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();
        builder.Services.AddSingleton<IRegistrationService, RegistrationService>();


        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };
        });



        builder.Services.AddScoped<EFGenericRepository<User>>();
        builder.Services.AddScoped<EFGenericRepository<Content>>();
        builder.Services.AddScoped<EFGenericRepository<Comment>>();
        builder.Services.AddScoped<EFGenericRepository<Post>>();
        builder.Services.AddScoped<EFGenericRepository<Issue>>();

        builder.Services.AddScoped<PostDataHandler>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProjectForum", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                       Reference = new OpenApiReference()
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();

        app.UseHttpLogging();

        app.MapControllers();

        app.MapGet("/users", async (Context db) => await db.Users.ToListAsync());

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.MapRazorPages();  //без этого не будет страниц

        app.Run();
    }
}
