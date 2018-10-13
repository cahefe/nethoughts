using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoWebAPI.Core.Models;

namespace TodoWebAPI.Infra.Repo.EFTodo
{
    public partial class TodoWebAPIContext : DbContext
    {
        static IConfiguration Configuration;
        public TodoWebAPIContext(IConfiguration configuration)
        {
            if (Configuration == null)
                Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("TodoWebAPIContext"));
        }

        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<Person> Person { get; set; }
    }
}
