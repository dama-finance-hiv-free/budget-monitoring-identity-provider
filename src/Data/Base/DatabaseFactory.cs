using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IdentityProvider.Data.Base
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        public DatabaseFactory(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
            _db = new NpgsqlConnection(GetContext().Database.GetDbConnection().ConnectionString);
        }

        private readonly ApplicationDbContext _dataContext;
        private readonly IDbConnection _db;

        public ApplicationDbContext GetContext()
        {
            return _dataContext;
        }

        public IDbConnection GetConnection()
        {
            return _db;
        }

        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
            _db.Dispose();
        }
    }

    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext GetContext();
        IDbConnection GetConnection();
    }
}