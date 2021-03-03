using Microsoft.EntityFrameworkCore;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Repositories
{
    public class NegociacaoDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public NegociacaoDBContext(DbContextOptions<NegociacaoDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NegociacaoDBContext).Assembly);
        }
    }
}