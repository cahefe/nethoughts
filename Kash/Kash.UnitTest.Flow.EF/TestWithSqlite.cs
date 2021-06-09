using System;
using Kash.Repository.Flow.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Kash.UnitTest.Flow.EF
{
    //  Ref: Unit testing in ASP.NET Core with EF Sqlite in-memory and XUnit: https://raaaimund.github.io/tech/2019/05/07/aspnet-core-unit-testing-inmemory/
    public abstract class TestWithSqlite : IDisposable
    {
        const string InMemoryConnectionString = "DataSource=:memory:";
        readonly SqliteConnection _connection;

        protected readonly RepositoryFlow DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<RepositoryFlowContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new RepositoryFlow(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose() => _connection.Close();
    }
}
