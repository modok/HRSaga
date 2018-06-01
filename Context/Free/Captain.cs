using System;
using Microsoft.Extensions.Logging;
using HRSaga.Entity.Characters;
namespace HRSaga.Context.Free
{
    public class Captain : Entity.Characters.CaptainGeneric
    {
         public Captain(ILogger logger) : base(logger)
        {
            
        }

        public Captain(Context.Tavern.Captain _captain): base(_captain)
        {
            
        }

        public Captain(Context.Mission.Captain _captain) : base(_captain)
        {

        }


        public void hire(Warrior warrior){
            hireCharacter(warrior);
        }

        public void hire(Wizard wizard)
        {
            hireCharacter(wizard);
        }

        public bool squadIsReady(){
            return squad.Count == maxSquadDimension;
        }

        public Tavern.Captain goToTavern(Entities.Tavern tavern){
            if (!squadIsReady())
                throw new SquadNotReady();
            
            return new Tavern.Captain(this,tavern);                 
            
        }

        public class SquadNotReady : Exception { }






    }
}