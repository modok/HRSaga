using System;
using System.Collections.Generic;

namespace HRSaga.Entities
{
    public class Tavern
    {
        public Guid id { get; protected set; }
        private List<MissionAvailable> bullettinBoard=new List<MissionAvailable>();

        //public Tavern(): this(new Guid())
        //{
        //}

        public Tavern()
        {
            id = new Guid();
            bullettinBoard.Add(new MissionAvailable(this));
            bullettinBoard.Add(new MissionAvailable(this));
        }

        public List<MissionAvailable> getMissionsInTheBoard(){
            return bullettinBoard; 
        }

        public Context.Mission.Captain assignTheMissionTotheCaptain(MissionAvailable mission, Context.Tavern.Captain captain){
            if(!bullettinBoard.Remove(mission)){
                throw new MissionMissing();    
            }
            Console.WriteLine("The captain accepted the mission!");

            return new Context.Mission.Captain(new MissionAssiged(mission, captain));

        }

        public Context.Free.Captain exit(Context.Tavern.Captain captain){
            return new Context.Free.Captain(captain);
        }
    }

    public class MissionMissing : Exception { }
}
