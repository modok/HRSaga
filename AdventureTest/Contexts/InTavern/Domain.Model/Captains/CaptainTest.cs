using System;
using HRSaga.Adventure.Context.InTavern.Domain.Model.Captains;
using Xunit;

namespace HRSaga.AdventureTest.Context.InTavern.Domain.Model.Captains
{
    public class CaptainTest
    {
        [Fact]
        public void A_captain_can_stay_in_the_tavern()
        {
            var sut = CreateSUT();
            Assert.IsType<Captain>(sut);
        }
        [Fact]
        public void A_captain_can_not_exist_with_more_than_5_squad_members()
        {
            int squadSize = 6;
    
            Assert.Throws<InvalidOperationException>(() => new Captain(squadSize));
        }

        [Fact]
        public void A_captain_can_sign_off_a_mission_when_the_squad_is_ready()
        {
            var sut = CreateSUT();
            Assert.IsType<Mission>(sut.signOff(new Mission()));
        }

        [Fact]
        public void A_captain_can_not_sign_off_a_mission_when_the_squad_is_not_ready()
        {
            int squadSize = 4;
            var sut = new Captain(squadSize);
            
            Assert.Throws<SignoffNotPossibleWithASquadNotReady>(() => sut.signOff(new Mission()));
        }

        [Fact]
        public void A_captain_can_not_sign_off_more_then_a_mission()
        {
            var sut = CreateSUT();
            sut.signOff(new Mission());
            Assert.Throws<CaptainHasAlreadySignedMission>(() => sut.signOff(new Mission()));
        }

        private Captain CreateSUT() {
            int squadSize = 5;
            return new Captain(squadSize);
        }
    }
}
