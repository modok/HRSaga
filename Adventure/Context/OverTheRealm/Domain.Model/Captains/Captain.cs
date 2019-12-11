using System;
using System.Collections.Generic;
using CQRSlite.Domain;
using HRSaga.Adventure.Context.Common.Domain.Model;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Captain : AggregateRoot
    {
        private CaptainId CaptainId  { get;  set; }
        private List<ICharacter> Squad { get; set; }
        
        public Captain(CaptainId captainId){
            Console.WriteLine("Aggregato capitano creato");
            AssertionConcern.AssertArgumentNotNull(captainId,"CaptainId null");
            this.CaptainId = captainId;
            this.Id=captainId.Id;
            //?? da capire meglio
            ApplyChange(new CaptainCreated(this.CaptainId));
        }
        public void hire(ICharacter character){
            if(isSquadFull()){
                throw new TooManyMembersException();
            }
            ApplyChange(new CharacterHired(this.CaptainId,character));
        }
        private bool isSquadFull(){
            return (this.Squad.Count == 5);
        }
        private void Apply(CaptainCreated e)
        {
            this.CaptainId=e.captainId();
            this.Squad = new List<ICharacter>();
        }
        private void Apply(CharacterHired e)
        {
            this.Squad.Add(e.Character);
        }
    }
}