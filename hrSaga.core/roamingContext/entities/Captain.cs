using System;
using hrSaga.core.infra;

namespace hrSaga.core.roamingContext.entities
{
    public class Captain : BaseEntity
    {
        public int SquadSize { get; set; }
        public bool IsValid { get; set; }

        public Captain(Guid id) : base(id)
        {
        }
    }
}
