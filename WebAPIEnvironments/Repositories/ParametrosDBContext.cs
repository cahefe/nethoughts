using Microsoft.EntityFrameworkCore;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Repositories
{
    public class ParametrosDBContext : DbContext
    {
        public DbSet<Parametro> Parametros { get; set; }
        public ParametrosDBContext(DbContextOptions<ParametrosDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParametrosDBContext).Assembly);
        }
    }
}