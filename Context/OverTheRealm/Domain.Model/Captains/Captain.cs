using System;
using HRSaga.Context.Common.Domain.Model;
using HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains
{
    public class Captain : Entity, IEquatable<Captain>
    {
        public CaptainId CaptainId  { get; private set; }
        public Squad Squad { get; private set; }

        public Captain(CaptainId captainId,Squad squad){
            this.CaptainId=captainId;
            this.Squad = squad;
        }

        public Captain(CaptainId captainId){
            this.CaptainId=captainId;
            this.Squad = new Squad(captainId);
        }

        
        public bool Equals(Captain other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(null, other)) return false;

            return this.CaptainId.Equals(other.CaptainId)
            && this.Squad.Equals(other.Squad);
        }

        public bool isSquadReady(){
            return this.Squad.isReady();
        }

        public void hire(ICharacter character){
            this.Squad.addCharacter(character);
        }

    }
}