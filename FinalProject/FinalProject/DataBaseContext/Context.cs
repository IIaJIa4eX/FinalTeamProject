using DatabaseConnector;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Models.DTO;

namespace FinalProject.DataBaseContext;

public class Context : DbContext
{

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<AccountSession> AccountSessions { get; set; }
    public DbSet<Content> Content { get; set; }

    public Context(DbContextOptions options) : base(options)
    {

    }

    public DbSet<FinalProject.Models.DTO.UserDto>? UserDto { get; set; }
}
