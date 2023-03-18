using DatabaseConnector;
//using DatabaseConnector.Migrations;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Extensions;
using Pomelo.EntityFrameworkCore.MySql;

namespace FinalProject.DataBaseContext;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<SessionInfo> SessionInfo { get; set; }
    public DbSet<Content> Content { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
        
    }
}
