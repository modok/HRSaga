using System;
using HRSaga.Context.Common.Domain.Model;
using HRSaga.Context.OverTheRealm.Domain.Model.Squads;

namespace HRSaga.Context.OverTheRealm.Domain.Model
{
    public class Captain : Entity, IEquatable<Captain>
    {
        public Captain(CaptainId captainId,Squad squad){
            this.CaptainId=captainId;
            this.Squad = squad;
        }
        public CaptainId CaptainId  { get; private set; }
        public Squad Squad {get; private set;}

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

    }
}