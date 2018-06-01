using System;
using HRSaga.Entity.Characters;

namespace HRSaga.Entities
{
    public class MissionAssiged: MissionGeneric
    {
        public CaptainGeneric captain { get; protected set; }
        public Tavern tavern { get; protected set; }

        public MissionAssiged(MissionAvailable _mission, Context.Tavern.Captain _captain):base()
        {
            captain = _captain;
            tavern = _mission.tavern;
        }
    }
}
