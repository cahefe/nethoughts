using System;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Entity;
using Corp.System.Hexagonal.Orders.Adapters.Repository.EF.Helpers;
using Corp.System.Hexagonal.Shared.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Corp.System.Hexagonal.Orders.Adapters.Repository.EF
{
    public class OrdersContext : DbContext
    {
        // readonly IConfiguration _configuration;
        readonly OrdersContextSettings _orderContextSettings;
        public OrdersContext(IConfiguration configuration)
        {
            var config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _orderContextSettings = config.GetAppSection<OrdersContextSettings>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(o => new { o.IDOrder, o.IDBond });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_orderContextSettings.RepoType == EnumRepoType.InMemory)
                optionsBuilder.UseInMemoryDatabase(databaseName: _orderContextSettings.DBName);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
