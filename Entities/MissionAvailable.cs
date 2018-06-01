using System;
using HRSaga.Entity.Characters;

namespace HRSaga.Entities
{
    public class MissionAvailable: MissionGeneric
    {
        public Tavern tavern { get; protected set; }

        public MissionAvailable(Tavern _tavern): base()
        {
            tavern = _tavern;
        }
    }
}
