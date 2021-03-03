using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebAPIEnvironments.Abstracts
{
    //  Referência: https://raaaimund.github.io/tech/2019/05/07/aspnet-core-unit-testing-inmemory/
    public abstract class DBContext_Tests_InMemory<TDBContext, TType> : IDisposable where TDBContext : DbContext
    {
        const string InMemoryConnectionString = "DataSource=:memory:";
        readonly SqliteConnection _connection;
        protected readonly TDBContext Context;
        protected readonly ILogger<TType> _logger;
        protected DBContext_Tests_InMemory(ILogger<TType> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<TDBContext>()
                .UseSqlite(_connection)
                .Options;
            Context = (TDBContext)Activator.CreateInstance(typeof(TDBContext), options);
            Context?.Database.EnsureCreated();
        }

        public void Dispose() => _connection.Close();
    }
}