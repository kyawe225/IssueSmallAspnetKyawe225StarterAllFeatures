using IssuePj.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssuePJ.Api.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    // Define your DbSets (tables) here
    public DbSet<Issue> Issues { get; set; }
}