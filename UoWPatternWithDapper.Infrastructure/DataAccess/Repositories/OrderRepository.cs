using System;
using System.Data;
using UoWPatternWithDapper.Domain.DataAccess.Repositories;
using UoWPatternWithDapper.Domain.Entities;
using UoWPatternWithDapper.Infrastructure.DataAccess.Repositories.Base;

namespace UoWPatternWithDapper.Infrastructure.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        {

        }
    }
}
