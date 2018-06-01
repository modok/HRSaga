using System;
using Microsoft.Extensions.Logging;

namespace HRSaga.Context.Mission
{
    public class Captain : Entity.Characters.CaptainGeneric
    {
        private Entities.MissionAssiged mission;

        public Captain(Entities.MissionAssiged _mission) : base(_mission.captain)
        {
            mission = _mission;
        }

        public Context.Free.Captain missionCompleted(){
            if (!mission.completed())
            {
                throw new MissionNotCompletedYet();
            }
            this.gold += mission.getReward();
            Console.WriteLine("Captain finished the mission and now has {0} gold", gold);
            return new Context.Free.Captain(this);
        }

    }
    public class MissionNotCompletedYet : Exception { }
}
