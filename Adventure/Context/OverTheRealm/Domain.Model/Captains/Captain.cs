using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Exceptions;
using HRSaga.Adventure.Context.Common.Domain.Model;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using Stateless;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Captain : AggregateRoot<Captain, CaptainId>,
            IEmit<CaptainCreated>,
            IEmit<WizardHired>,
            IEmit<WarriorHired>
    {
        private List<ICharacter> _squad { get; set; }
        
        public Captain(CaptainId captainId):base(captainId){
            //stateSetup(State.OverTheRealm);
            //AssertionConcern.AssertArgumentNotNull(captainId,"CaptainId null");
            _squad=new List<ICharacter>();
            //Emit(new CaptainCreated());
        }

         
        public void hire(Warrior warrior){
            if(isSquadFull()){
                throw DomainError.With("Squad is full");
            }
            Emit(new WarriorHired(warrior));
        }

        public void hire(Wizard wizard){
            if(isSquadFull()){
                throw DomainError.With("Squad is full");
            }
            Emit(new WizardHired(wizard));
        }
        private bool isSquadFull(){
            return (_squad.Count == 5);
        }

        void IEmit<CaptainCreated>.Apply(CaptainCreated aggregateEvent)
        {
            _squad = new List<ICharacter>();
        }

        public void Apply(WarriorHired aggregateEvent)
        {
            _squad.Add(aggregateEvent.Warrior);
        }

        public void Apply(WizardHired aggregateEvent)
        {
            _squad.Add(aggregateEvent.Wizard);
        }




        //private StateMachine<State,Activity> StateMachine;
        //enum State { OverTheRealm, InTavern, InMission }
        //enum Activity { Hire, goToTavern, SubscribeMission, CompleteMission, returnOverTheReal }
        /* private void stateSetup(State initialState){
            //InitialState
             StateMachine = new StateMachine<State,Activity>(initialState);
            //OverTheRealm
            StateMachine.Configure(State.OverTheRealm)
            .Permit(Activity.Hire,State.OverTheRealm)
            .Permit(Activity.goToTavern,State.InTavern);
            //InTavern
            StateMachine.Configure(State.InTavern)
            .PermitIf(Activity.SubscribeMission,State.InMission,()=>isSquadFull())
            .Permit(Activity.returnOverTheReal,State.OverTheRealm);
            //InMission
            StateMachine.Configure(State.InTavern)
            .Permit(Activity.CompleteMission,State.OverTheRealm);

        } */
    }
}