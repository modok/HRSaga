using System;
using System.Collections.Generic;
using RoamingContext = Adventure.RoamingContext;
using Microsoft.Extensions.Logging;
using System.Linq;
using Adventure.Shared;

namespace Adventure.TavernContext
{
    public class NotFullSquadException : Exception
    {

    }

    public class Captain : CaptainBase
    {
    
        public Captain(ILogger logger) : this(Guid.NewGuid(), new List<Adventurer>(), null, 0, logger)
        {
        }

        public Captain(Guid id, IReadOnlyList<Adventurer> squad, ILogger logger) : this(id, squad, null, 0, logger)
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

            _logger.LogInformation("the captain entered at the tavern...");
        }


        public void SignOnMission()
        {
            if (_squad.Count < 5)
            {
                _logger.LogInformation("the captain had not a squad to sign on a mission");
                throw new NotFullSquadException();
            }

            Mission = new Mission();
            _logger.LogInformation("the captain signed on a mission");
        }

        public RoamingContext.Captain Exit()
        {
            _logger.LogInformation("the captain left the tavern");
            Location = null;
            return new RoamingContext.Captain(this, _logger);
        }
    }
}