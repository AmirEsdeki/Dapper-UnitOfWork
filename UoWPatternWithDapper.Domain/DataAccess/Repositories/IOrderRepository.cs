using System;
using UoWPatternWithDapper.Domain.DataAccess.Repositories.Base;
using UoWPatternWithDapper.Domain.Entities;

namespace UoWPatternWithDapper.Domain.DataAccess.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order, Guid>
    {
    }
}
