using System;
using System.Collections.Generic;
using CQRSlite.Domain;
using HRSaga.Adventure.Context.Common.Domain.Model;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using Stateless;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Captain : AggregateRoot
    {
            enum State { OverTheRealm, InTavern, InMission }
            enum Activity { Hire, goToTavern, SubscribeMission, CompleteMission, returnOverTheReal }

        private List<ICharacter> Squad { get; set; }
        //private StateMachine<State,Activity> StateMachine;
        
        public Captain(CaptainId captainId):base(captainId){
            //stateSetup(State.OverTheRealm);
            AssertionConcern.AssertArgumentNotNull(captainId,"CaptainId null");
            ApplyChange(new CaptainCreated(CaptainId()));
        }

        public CaptainId CaptainId(){
            return (CaptainId)Identity;
        }

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

        //public void CaptainId(CaptainId captainId){
        //    this.Id=captainId.Id;
        //}

        public void hire(ICharacter character){
            if(!isSquadFull()){
                ApplyChange(new CharacterHired(CaptainId(),character));    
            }
        }
        private bool isSquadFull(){
            return (this.Squad.Count == 5);
        }
        private void Apply(CaptainCreated e)
        {
            this.Identity=e.captainId();
            this.Squad = new List<ICharacter>();
        }
        private void Apply(CharacterHired e)
        {
            this.Squad.Add(e.Character);
        }
    }
}