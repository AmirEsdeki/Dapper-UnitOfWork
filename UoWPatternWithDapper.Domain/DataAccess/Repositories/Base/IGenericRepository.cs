using System.Collections.Generic;
using System.Threading.Tasks;
using UoWPatternWithDapper.Domain.Entities.Base;

namespace UoWPatternWithDapper.Domain.DataAccess.Repositories.Base
{
    /// <summary>
    /// Does basic crud operations on any entity which is of type BaseEntity.
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    /// <typeparam name="Y">Type of identification field</typeparam>
    public interface IGenericRepository<T,Y> where T : BaseEntity where Y : struct
    {
        Task<long> CountAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetManyAsync(IEnumerable<Y> ids);
        Task<T> GetOneAsync(Y id);
        Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities);
        Task<T> CreateOneAsync(T entity);
        Task<T> UpdateOneAsync(T entity);
        Task DeleteAllAsync();
        Task DeleteManyAsync(IEnumerable<Y> ids);
        Task DeleteOneAsync(Y id);
    }
}
