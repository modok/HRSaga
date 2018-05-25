using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Shared;
using Microsoft.Extensions.Logging;

namespace Adventure.RoamingContext
{

    public class Captain : CaptainBase
    {
        public Captain(ILogger logger) : this(Guid.NewGuid(), new List<Adventurer>(), null, 0, logger)
        {
        }

        public Captain(CaptainBase captain, ILogger logger) : this(captain.id, captain.Squad, captain.Mission, captain.Gold, logger)
        {

        }

        private Captain(Guid _id, IEnumerable<Adventurer> squad, Mission mission, int gold, ILogger logger)
        {
            id = _id;
            _squad = squad.ToList();
            Mission = mission;
            Gold = gold;
            Location = new Tavern();
            _logger = logger;

            _logger.LogInformation("the captain was roaming for the kingdom");
        }

        public void Hire(Adventurer adventurer)
        {
            if (_squad.Count == 5) {
                 _logger.LogInformation("the captain tried to hire more people he could pay");
                throw new FullSquadException();
            }

            _squad.Add(adventurer);
            _logger.LogInformation("the captain hired a new member!");
            _logger.LogInformation("the captain had {0} members in his squad", Squad.Count());
        }

        public TavernContext.Captain GoToTheTavern()
        {
             _logger.LogInformation("the captain went to the tavern...");
            return new TavernContext.Captain(this, _logger);
        }

        public void CompleteTheMission()
        {
            _logger.LogInformation("the captain completed a mission!");
            _logger.LogInformation("the captain earned 10 gp");
            Gold += 10;
            _logger.LogInformation("the captain owned at that moment {0} gp", Gold);
            Mission = null;
        }
    }

    public class FullSquadException : Exception { }
}