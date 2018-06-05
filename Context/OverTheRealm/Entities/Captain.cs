using System;
using System.Collections.Generic;
using HRSaga.Entities;
using HRSaga.ValueObjects;

namespace HRSaga.Context.OverTheRealm.Entities
{
    public class Captain : CaptainBase
    {
        public Captain(): base(new Guid(), new List<Character>(), 0)
        {
            
        }

        public Captain(InMission.Entities.Captain captain): base(captain)
        {
            
        }

        public Captain(Context.InTheTavern.Entities.Captain captain) : base(captain)
        {

        }

        private void HireCharacter(Character character)
        {
            if (_squad.Count == _maxSquadDimension)
            {
                throw new FullSquadException();
            }
            _squad.Add(character);
            Console.WriteLine("the captain hired a new member!");
            Console.WriteLine("the captain had {0} members in his squad", _squad.Count);
        }


        public void Hire(Warrior warrior){
            HireCharacter(warrior);
        }

        public void Hire(Wizard wizard)
        {
            HireCharacter(wizard);
        }

        public bool SquadIsReady(){
            return _squad.Count == _maxSquadDimension;
        }

        public InTheTavern.Entities.Captain goToTavern(Tavern tavern){
            if (!SquadIsReady())
                throw new SquadNotReady();
            
            return new InTheTavern.Entities.Captain(this,tavern);                 
            
        }








    }
}