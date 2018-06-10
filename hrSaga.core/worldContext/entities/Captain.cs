using System;
using hrSaga.core.infra;
using hrSaga.core.worldContext.enums;

namespace hrSaga.core.worldContext.entities
{
    public class Captain : BaseEntity
    {
        public Location Location { get; set; }

        public Captain(Guid id) : base(id)
        {
        }
    }
}
