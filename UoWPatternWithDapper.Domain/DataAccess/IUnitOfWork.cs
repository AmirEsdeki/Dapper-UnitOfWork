using System;
using UoWPatternWithDapper.Domain.DataAccess.Repositories;

namespace UoWPatternWithDapper.Domain.DataAccess
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }
    }
}