using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace HRSaga.Entity.Characters
{
    public abstract class CaptainGeneric : Character
    {
        //protected List<Warrior> warriors;
        protected List<Character> squad;
        public int gold { get; protected set; }
        protected int maxSquadDimension = 5;

        public CaptainGeneric(ILogger logger): this(Guid.NewGuid(),new List<Character>(),0, logger)
        {
            
        }

        public CaptainGeneric(CaptainGeneric _captain):this(_captain.id,_captain.squad,_captain.gold,_captain.logger)
        {
            
        }

        private CaptainGeneric(Guid _id, List<Character> _squad, int _gold, ILogger _logger){
            id = _id;
            logger = _logger;
            squad = _squad;
            gold = _gold;

        }

        protected void hireCharacter(Character character)
        {
            if (squad.Count == maxSquadDimension)
            {
                throw new FullSquadException();
            }
            squad.Add(character);
            Console.WriteLine("the captain hired a new member!");
            Console.WriteLine("the captain had {0} members in his squad", squad.Count);
        }

    }

    public class FullSquadException : Exception { }

}