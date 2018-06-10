using System;

namespace hrSaga.core.infra
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
