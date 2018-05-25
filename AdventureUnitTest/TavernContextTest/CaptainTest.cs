using System;
using Xunit;
using TavernContext = Adventure.TavernContext;
using RoamingContext = Adventure.RoamingContext;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Adventure.Shared;

namespace AdventureUnitTest.TavernContextTest
{
    public class CaptainTest: IDisposable
    {
        private ILogger nullLogger;
        public CaptainTest()
        {
            nullLogger = new NullLogger<ILogger>();
        }

        [Fact]
        public void A_captain_shouldnt_be_able_to_sign_for_a_mission_without_a_full_squad()
        {
            var captain = new TavernContext.Captain(nullLogger);
            Assert.Throws<TavernContext.NotFullSquadException>(() => captain.SignOnMission());
        }

        [Fact]
        public void A_captain_should_be_able_to_sign_for_a_mission_with_a_full_squad()
        {
            var captain = new TavernContext.Captain(Guid.NewGuid(), new List<Adventurer>() { new Warrior(), new Warrior(), new Warrior(), new Warrior(), new Warrior()}, nullLogger);
            Assert.Null(captain.Mission);
            captain.SignOnMission();
            Assert.NotNull(captain.Mission);
        }

        [Fact]
        public void A_captain_should_be_able_to_exit_the_tavern()
        {
            var captain = new TavernContext.Captain(nullLogger);
            Assert.IsType<Tavern>(captain.Location);
            var newContextCaptain = captain.Exit();
            Assert.Null(captain.Location);
            Assert.IsType<RoamingContext.Captain>(newContextCaptain);
        }

        public void Dispose()
        {
            nullLogger = null;
        }
    }
}
