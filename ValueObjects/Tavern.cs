using System;
using System.Collections.Generic;

namespace HRSaga.ValueObjects
{
    public class Tavern
    {
        private List<Mission> bullettinBoard = new List<Mission>();

        public Tavern()
        {
            bullettinBoard.Add(new Mission());
            bullettinBoard.Add(new Mission());
        }

        public List<Mission> ShowMissionsInTheBoard()
        {
            return bullettinBoard;
        }

        public Context.InMission.Entities.Captain assignTheMissionTotheCaptain(Mission mission, Context.InTheTavern.Entities.Captain captain)
        {
            if (!bullettinBoard.Remove(mission))
            {
                throw new MissionMissing();
            }
            Console.WriteLine("The captain accepted the mission!");

            return new Context.InMission.Entities.Captain(captain,mission);

        }

        public Context.OverTheRealm.Entities.Captain exit(Context.InTheTavern.Entities.Captain captain)
        {
            return new Context.OverTheRealm.Entities.Captain(captain);
        }
    }

    public class MissionMissing : Exception { }
}
