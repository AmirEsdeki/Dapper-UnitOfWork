using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using UoWPatternWithDapper.Domain.Entities.Base;

namespace UoWPatternWithDapper.Infrastructure.DataAccess.Repositories.Base
{
    public abstract class GenericRepositoryBase<T> where T : BaseEntity
    { 
        protected readonly IDbTransaction transaction;
        protected readonly string tableName;
        protected readonly IDbConnection connection;
        private IEnumerable<PropertyInfo> entityProperties => typeof(T).GetProperties();

        public GenericRepositoryBase(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
            this.tableName = nameof(T);
        }

        protected string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfPropertiesToInsert(entityProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") OUTPUT INSERTED.* VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        protected string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfPropertiesToUpdate(entityProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id OUTPUT INSERTED.*");

            return updateQuery.ToString();
        }

        private static List<string> GenerateListOfPropertiesToInsert(IEnumerable<PropertyInfo> listOfProperties)
        {
            var result = new List<string>();
            foreach (var item in listOfProperties)
            {
                var attributes = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var name = item.Name;
                if (!name.Contains("Id") && (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"))
                {
                    result.Add(name);
                }
            }
            return result;
        }

        private static List<string> GenerateListOfPropertiesToUpdate(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description.ToLower() != "ignore"
                    select prop.Name).ToList();
        }
    }
}
