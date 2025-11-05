using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace  DemoApp.Infrastructure.Persistence
{
    public class DemoAppDbContext : DbContext
    {
        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options):base(options){}


        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoAppDbContext).Assembly);
        }
    }
};

