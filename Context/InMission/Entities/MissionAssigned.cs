using System;
using HRSaga.ValueObjects;

namespace HRSaga.Context.InMission.Entities
{
    public class MissionAssigned
    {
        private Mission _mission;
        private String status = "TODO";

        public MissionAssigned(Mission mission)
        {
            _mission = mission;
        }

        public bool Completed()
        {
            status = "DONE";
            Console.WriteLine("Mission completed!!");
            return true;
        }

        public int getReward()
        {
            if (status == "DONE")
            {
                return _mission.GetReward();
            }
            else
            {
                return 0;
            }
        }
    }
}
