using DatabaseConnector;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DataBaseContext;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<SessionInfo> SessionInfo { get; set; }

    public Context(DbContextOptions<Context> options) : base(options) {  }
}
