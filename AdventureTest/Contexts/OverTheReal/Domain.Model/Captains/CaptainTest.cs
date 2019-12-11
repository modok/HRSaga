using System;
using Xunit;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;

namespace HRSaga.AdventureTest.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainTest
    {
        [Fact]
        public void A_new_captain_can_exist_in_the_world()
        {
            var sut = CreateSUT();
            Assert.IsType<Captain>(sut);
        }

        [Fact]
        public void Captain_hire_a_warrior(){
            var sut = CreateSUT();
          //  var hired= sut.hire(new Warrior());
          //  Assert.IsType<Warrior>(hired);
          //  Assert.IsNotType<Wizard>(hired);
        }

        [Fact]
        public void Captain_hire_a_wizard(){
            var sut = CreateSUT();
           // var hired= sut.hire(new Wizard());
          //  Assert.IsType<Wizard>(hired);
          //   Assert.IsNotType<Warrior>(hired);
        }

        [Fact]
        public void Captain_cant_hire_more_than_5_memebers() {
            var sut = CreateSUT();
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());

            Assert.Throws<TooManyMembersException>(() => sut.hire(new Warrior()));
        }
        [Fact]
        public void Captatin_can_go_to_the_tavern(){
            var sut = CreateSUT();
            //Assert.IsType<Warrior>(hired);
            //sut.goToTheTavern()

        }

        private Captain CreateSUT() {
            return new Captain(new CaptainId());
        }
    }
}
