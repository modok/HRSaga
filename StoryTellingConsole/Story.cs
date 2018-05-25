using RoamingContext = Adventure.RoamingContext;
using Adventure.Shared;
using Microsoft.Extensions.Logging;

namespace StoryTellingConsole
{
    public class Story 
    {
        private ILogger _logger;
        public Story(ILogger logger)
        {
            _logger = logger;
        }
        public void TellMeAboutTheCaptainAndHisFirstMission() 
        {
            _logger.LogInformation("This is the story of a captain and his first mission");
            var captain = new RoamingContext.Captain(_logger);
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            var captainInTheTavern = captain.GoToTheTavern();
            captainInTheTavern.SignOnMission();
            captain = captainInTheTavern.Exit();
            captain.CompleteTheMission();
            int gold = captain.Gold;
            
            _logger.LogInformation("End of this story");
        }
    }
}