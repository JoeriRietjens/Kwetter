using System;
using System.Threading.Tasks;
using kwetter_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace kwetter_backend.Data
{
    public class KweetContext : DbContext
    {
        public DbSet<Kweet> Kweets { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
