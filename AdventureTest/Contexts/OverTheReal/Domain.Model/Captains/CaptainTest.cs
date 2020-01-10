using System;
using Xunit;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using System.Linq;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using System.Collections.Generic;
using EventFlow.Exceptions;

namespace HRSaga.AdventureTest.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainTest
    {
        private Captain _sut;

        [Fact]
        public void A_new_captain_can_exist_in_the_world()
        {

            var sut = CreateSUT();
            sut.initCaptain();
            var aggregateEvent = sut.UncommittedEvents.First().AggregateEvent;
            Assert.IsType<CaptainCreated>(aggregateEvent);
            sut.Apply((CaptainCreated)aggregateEvent);
            Assert.Equal(0,sut.squadSize());
            Assert.IsType<Captain>(sut);
        }

        [Fact]
        public void Captain_hire_a_warrior(){
            var sut = EmptySquadSUT();

            sut.hire(new Warrior());
            var uncommitted = sut.UncommittedEvents;
            var aggregateEvent = sut.UncommittedEvents.First().AggregateEvent;
            Assert.IsType<WarriorHired>(aggregateEvent);
            //sut.Apply((WarriorHired)aggregateEvent);
            Assert.Equal(1,sut.squadSize());
        }

        [Fact]
        public void Captain_hire_a_wizard(){
            var sut = EmptySquadSUT();

            sut.hire(new Wizard());
            var uncommitted = sut.UncommittedEvents;
            var aggregateEvent = sut.UncommittedEvents.First().AggregateEvent;
            Assert.IsType<WizardHired>(aggregateEvent);
            //sut.Apply((WarriorHired)aggregateEvent);
            Assert.Equal(1,sut.squadSize());
        }

        [Fact]
        public void Captain_cant_hire_more_than_5_memebers() {
            var sut = EmptySquadSUT();

            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            sut.hire(new Warrior());
            Assert.Equal(5,sut.squadSize());
            Assert.Throws<DomainError>(() => sut.hire(new Warrior()));
        }

        [Fact]
        public void Captatin_can_go_to_the_tavern(){
            var sut = CreateSUT();
            //Assert.IsType<Warrior>(hired);
            //sut.goToTheTavern()

        }

        private Captain CreateSUT() {
            return new Captain(CaptainId.New);
        }

        private Captain EmptySquadSUT(){
            Captain captainEmpty=CreateSUT();
            captainEmpty.Apply(new CaptainCreated(new List<ICharacter>()));
            return captainEmpty;
        }
    }
}
