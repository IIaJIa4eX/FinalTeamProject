using DatabaseConnector;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DataBaseContext;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Issue> Issues { get; set; }
<<<<<<< HEAD
    public DbSet<AccountSession> AccountSessions { get; set; }
    public DbSet<Content> Content { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public Context(DbContextOptions options) : base(options) { }
   
=======
    public DbSet<SessionInfo> SessionInfo { get; set; }
    public DbSet<Content> Content { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
        
    }
>>>>>>> IVANdev
}
