using System;
using hrSaga.core.infra;

namespace hrSaga.core.tavernContext.entities
{
    public class Captain : BaseEntity
    {
        public int SquadSize { get; set; }
        public bool IsMissionSignedOff { get; set; }
        public bool IsMissionStarted { get; set; }
        public bool IsValid { get; set; }

        public Captain(Guid id) : base(id)
        {
        }
    }
}
