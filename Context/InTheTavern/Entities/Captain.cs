using System;
using System.Collections.Generic;
using HRSaga.Entities;
using HRSaga.ValueObjects;

namespace HRSaga.Context.InTheTavern.Entities
{
    public class Captain : CaptainBase
    {
        private Tavern _tavern;

        public Captain(OverTheRealm.Entities.Captain captain, Tavern tavern):base(captain)
        {
            _tavern = tavern;
            Console.WriteLine("Captain in tavern!");
        }


        public List<Mission> LookTheMissionOnTavernBoard()
        {
            Console.WriteLine("The board contains {0} missions!", _tavern.ShowMissionsInTheBoard().Count);
            return _tavern.ShowMissionsInTheBoard();
        }

        public InMission.Entities.Captain acceptMission(Mission mission)
        {
            return _tavern.assignTheMissionTotheCaptain(mission, this);
        }

        public OverTheRealm.Entities.Captain exitWithoutMission()
        {
            return _tavern.exit(this);
        }
    }

}
