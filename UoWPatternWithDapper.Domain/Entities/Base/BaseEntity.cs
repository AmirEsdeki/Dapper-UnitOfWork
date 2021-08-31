using System;

namespace UoWPatternWithDapper.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
