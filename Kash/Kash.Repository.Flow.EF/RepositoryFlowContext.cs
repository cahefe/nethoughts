using Kash.Domain.Flow.Model;
using Microsoft.EntityFrameworkCore;

namespace Kash.Repository.Flow.EF
{
    public class RepositoryFlowContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public RepositoryFlowContext(DbContextOptions<RepositoryFlowContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryFlowContext).Assembly);
        }
    }
}
