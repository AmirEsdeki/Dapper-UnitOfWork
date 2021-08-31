using System.Data;
using UoWPatternWithDapper.Domain.DataAccess;
using UoWPatternWithDapper.Domain.DataAccess.Repositories;
using UoWPatternWithDapper.Infrastructure.DataAccess.Repositories;

namespace UoWPatternWithDapper.Infrastructure.DataAccess
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(string connectionString, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) : base(connectionString, isolationLevel) { }
        public IProductRepository Products { get => new ProductRepository(connection, transaction);}
        public IOrderRepository Orders { get => new OrderRepository(connection, transaction); }
    }
}
