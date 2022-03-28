using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace SIG2Server.Models;

public class ModelsContext : DbContext
{
    public ModelsContext(DbContextOptions<ModelsContext> options)
            : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        var connectionString = configuration.GetConnectionString("AppDb");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OM>().HasKey(o => o.Id);
        modelBuilder.Entity<Software>().HasKey(s => s.Id);
        modelBuilder.Entity<SoftwareOM>().HasKey(t => t.Id);
        
        modelBuilder.Entity<OM>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        modelBuilder.Entity<Software>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        modelBuilder.Entity<SoftwareOM>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
    }

    public DbSet<OM> OMs { get; set; } = null!;
    public DbSet<Software> Softwares { get; set; } = null!;
    public DbSet<SoftwareOM> SoftwareOMs { get; set; } = null!;
}