using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace HRSaga.Context.Tavern
{
    public class Captain : Entity.Characters.CaptainGeneric
    {
        private Entities.Tavern tavern;
                        
        public Captain(Context.Free.Captain _captain, Entities.Tavern _tavern) : base(_captain)
        {
            tavern = _tavern;

            Console.WriteLine("Captain in tavern!");
        }


        public List<Entities.MissionAvailable> lookTheMissionOnTavernBoard(){
            Console.WriteLine("The board contains {0} missions!", tavern.getMissionsInTheBoard().Count);
            return tavern.getMissionsInTheBoard();
        }

        public Context.Mission.Captain acceptMission(Entities.MissionAvailable mission){
            return tavern.assignTheMissionTotheCaptain(mission,this);
        }

        public Context.Free.Captain exitWithoutMission(){
            return tavern.exit(this);
        }
    }
}
