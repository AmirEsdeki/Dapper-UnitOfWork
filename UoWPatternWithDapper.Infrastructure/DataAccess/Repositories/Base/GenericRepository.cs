using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UoWPatternWithDapper.Domain.DataAccess.Repositories.Base;
using UoWPatternWithDapper.Domain.Entities.Base;

namespace UoWPatternWithDapper.Infrastructure.DataAccess.Repositories.Base
{
    /// <summary>
    /// Does basic crud operations on any entity which is of type BaseEntity.
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    /// <typeparam name="Y">Type of identification field</typeparam>
    public class GenericRepository<T, Y> : GenericRepositoryBase<T> , IGenericRepository<T, Y> where T : BaseEntity where Y : struct
    {
        public GenericRepository(IDbConnection connection, IDbTransaction transaction) 
            : base(connection, transaction) { }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {tableName}";
            return await connection.QueryAsync<T>(query, transaction);
        }

        public async Task<IEnumerable<T>> GetManyAsync(IEnumerable<Y> ids)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id IN ({String.Join(',',ids)})";
            var result = await connection.QueryAsync<T>(query, transaction);
            return result;
        }

        public async Task<T> GetOneAsync(Y id)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id={id}";
            var result = await connection.QuerySingleOrDefaultAsync<T>(query, transaction);
            return result;
        }
        public async Task<long> CountAsync()
        {
            var query = $"SELECT COUNT(1) FROM {tableName}";
            return await connection.QuerySingleOrDefaultAsync<long>(query, transaction);
        }

        public async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            var query = GenerateInsertQuery();
            var inserted = await connection.QueryAsync<T>(query, entities, transaction);
            return inserted;
        }

        public async Task<T> CreateOneAsync(T entity)
        {
            var query = GenerateInsertQuery();
            var inserted = await connection.QuerySingleAsync<T>(query, entity, transaction);
            return inserted;
        }

        public async Task<T> UpdateOneAsync(T entity)
        {
            var query = GenerateUpdateQuery();
            var updated = await connection.QuerySingleAsync<T>(query, entity, transaction);
            return updated;
        }

        public async Task DeleteAllAsync()
        {
            var query = $"DELETE FROM {tableName}";
            await connection.ExecuteAsync(query, transaction);
        }

        public async Task DeleteManyAsync(IEnumerable<Y> ids)
        {
            var query = $"DELETE FROM {tableName} WHERE Id IN ({String.Join(',', ids)})";
            await connection.ExecuteAsync(query, transaction);
        }

        public async Task DeleteOneAsync(Y id)
        {
            var query = $"DELETE FROM {tableName} WHERE Id={id}";
            await connection.ExecuteAsync(query, new { Id = id }, transaction);
        }
    }
}
