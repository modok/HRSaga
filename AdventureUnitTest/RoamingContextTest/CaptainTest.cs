using System;
using Xunit;
using TavernContext = Adventure.TavernContext;
using RoamingContext = Adventure.RoamingContext;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using Adventure.Shared;

namespace AdventureUnitTest.RoamingContextTest
{
    public class CaptainTest : IDisposable
    {
        private ILogger nullLogger;
        public CaptainTest()
        {
            nullLogger = new NullLogger<ILogger>();
        }
        public void Dispose()
        {
            nullLogger = null;
        }

        [Fact]
        public void A_captain_should_be_able_to_hire_an_adventurer()
        {
            var captain = new RoamingContext.Captain(nullLogger);
            Assert.Empty(captain.Squad);
            captain.Hire(new Warrior());
            Assert.Single(captain.Squad);
        }

        [Fact]
        public void A_captain_shouldnt_be_able_to_hire_more_than_5_adventurer()
        {
            var captain = new RoamingContext.Captain(nullLogger);
            Assert.Empty(captain.Squad);
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            Assert.Equal(5, captain.Squad.Count());
            Assert.Throws<RoamingContext.FullSquadException>(() => captain.Hire(new Warrior()));
        }

        [Fact]
        public void A_captain_should_able_to_go_to_the_tavern()
        {
            var captain = new RoamingContext.Captain(nullLogger);
            var newContextCaptain = captain.GoToTheTavern();
            Assert.IsType<TavernContext.Captain>(newContextCaptain);
        }

        [Fact]
        public void A_captain_with_a_team_should_bring_the_team_to_the_tavern()
        {
            var captain = new RoamingContext.Captain(nullLogger);
            captain.Hire(new Warrior());
            var newContextCaptain = captain.GoToTheTavern();
            Assert.IsType<TavernContext.Captain>(newContextCaptain);
            Assert.Single(newContextCaptain.Squad);
        }

        [Fact]
        public void A_captain_should_be_able_to_solve_a_mission()
        {
            var captain = new RoamingContext.Captain(nullLogger);
            Assert.Null(captain.Mission);
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            captain.Hire(new Warrior());
            var captainInTheTavern = captain.GoToTheTavern();
            captainInTheTavern.SignOnMission();
            captain = captainInTheTavern.Exit();
            Assert.NotNull(captain.Mission);
            captain.CompleteTheMission();
            Assert.Null(captain.Mission);
            Assert.Equal(10, captain.Gold);
        }

    }
}
