using System;
using HRSaga.ValueObjects;
using HRSaga.Entities;


namespace HRSaga.Context.InMission.Entities
{
    public class Captain : CaptainBase
    {
        private MissionAssigned _missionAssigned;

        public Captain(InTheTavern.Entities.Captain captain, Mission mission) : base(captain)
        {
            _missionAssigned = new MissionAssigned(mission);
        }

        public OverTheRealm.Entities.Captain MissionCompleted(){
            if (!_missionAssigned.Completed())
            {
                throw new MissionNotCompletedYet();
            }
            _gold += _missionAssigned.getReward();
            Console.WriteLine("Captain finished the mission and now has {0} gold", _gold);
            return new OverTheRealm.Entities.Captain(this);
        }

    }
    public class MissionNotCompletedYet : Exception { }
}
