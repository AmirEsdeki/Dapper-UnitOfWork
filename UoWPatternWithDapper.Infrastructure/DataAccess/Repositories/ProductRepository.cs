using System;
using System.Data;
using UoWPatternWithDapper.Domain.DataAccess.Repositories;
using UoWPatternWithDapper.Domain.Entities;
using UoWPatternWithDapper.Infrastructure.DataAccess.Repositories.Base;

namespace UoWPatternWithDapper.Infrastructure.DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product,Guid>, IProductRepository
    {
        public ProductRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        {

        }
    }
}
