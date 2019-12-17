using System;
using System.Collections.Generic;
using CQRSlite.Domain;
using HRSaga.Adventure.Context.Common.Domain.Model;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Captain : AggregateRoot
    {
        private List<ICharacter> Squad { get; set; }
        
        
        public Captain(CaptainId captainId):base(captainId){
            Console.WriteLine("Aggregato capitano creato");
            AssertionConcern.AssertArgumentNotNull(captainId,"CaptainId null");
            ApplyChange(new CaptainCreated(CaptainId()));
        }

        public CaptainId CaptainId(){
            return (CaptainId)Identity;
        }

        //public void CaptainId(CaptainId captainId){
        //    this.Id=captainId.Id;
        //}

        public void hire(ICharacter character){
            if(isSquadFull()){
                ApplyEvent(new CaptainSquadFullFilled(CaptainId(),this.Squad.Count));
                return;
            }
            ApplyChange(new CharacterHired(CaptainId(),character));
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