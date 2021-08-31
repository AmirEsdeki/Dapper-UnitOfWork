using System;

namespace UoWPatternWithDapper.Domain.DataAccess
{
    public interface IUnitOfWorkBase : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
