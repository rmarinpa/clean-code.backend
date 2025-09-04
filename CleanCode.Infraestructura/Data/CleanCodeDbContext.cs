using CleanCode.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace CleanCode.Infraestructura.Data
{
    public class CleanCodeDbContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        public CleanCodeDbContext(DbContextOptions<CleanCodeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
        }
    }
}
