using System;
using System.Data;
using System.Data.SqlClient;
using UoWPatternWithDapper.Domain.DataAccess;

namespace UoWPatternWithDapper.Infrastructure.DataAccess
{
    public abstract class UnitOfWorkBase : IUnitOfWorkBase
    {
        protected bool disposed;
        protected readonly IDbConnection connection;
        protected readonly IDbTransaction transaction;

        public UnitOfWorkBase(string connectionString, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            connection = SqlClientFactory.Instance.CreateConnection();
            if (connection == null)
                throw new Exception("Error initializing connection");

            connection.ConnectionString = connectionString;

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                this.transaction = connection.BeginTransaction(isolationLevel);
            }
        }
        public void Commit() => transaction.Commit();
        public void Rollback() => transaction.Rollback();

        public void Dispose()
        {
            if (!disposed)
            {
                transaction.Dispose();
                connection.Dispose();
                disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}