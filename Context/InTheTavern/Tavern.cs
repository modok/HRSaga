using System;
using System.Collections.Generic;
using HRSaga.Artefacts;

namespace HRSaga.Context.InTheTavern
{
    public class Tavern : ValueObject
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


       /* public Context.InMission.Entities.Captain assignTheMissionTotheCaptain(Mission mission, Context.InTheTavern.Captain captain)
        {
            if (!bullettinBoard.Remove(mission))
            {
                throw new MissionMissing();
            }
            Console.WriteLine("The captain accepted the mission!");

            return new Context.InMission.Entities.Captain(captain,mission);

        }*/

        /*public Context.OverTheRealm.Captain exit(Context.InTheTavern.Captain captain)
        {
            return new Context.OverTheRealm.Captain(captain);
        }*/
    }

    public class MissionMissing : Exception { }
}
