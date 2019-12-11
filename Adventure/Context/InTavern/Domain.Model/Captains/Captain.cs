using System;
using System.Collections.Generic;
using HRSaga.Adventure.Context.Common.Domain.Model;

namespace HRSaga.Adventure.Context.InTavern.Domain.Model.Captains
{
    public class Captain : Entity, IEquatable<Captain>
    {
        private CaptainId CaptainId  { get;  set; }
        private int SquadSize  { get;  set; }

        private Mission Mission { get;  set; }

        public Captain(CaptainId captainId, int squadSize){
            AssertionConcern.AssertArgumentRange(squadSize,0,5,"SquadSizeWrong");
            this.CaptainId=captainId;
            this.SquadSize = squadSize;
            this.Mission = null;
        }

        public Captain(int squadSize):this(new CaptainId(),squadSize){
        }

        public Mission signOff(Mission mission){
            if(this.isMissionAssigned()){
                throw new CaptainHasAlreadySignedMission();
            }
            if(!this.isSquadReady()){
                throw new SignoffNotPossibleWithASquadNotReady();
            }
            this.Mission = mission;

            return this.Mission;

        }

        public bool Equals(Captain other)
        {
            if (object.ReferenceEquals(this, other)) return true;
            if (object.ReferenceEquals(null, other)) return false;

            return this.CaptainId.Equals(other.CaptainId);
        }

        private bool isSquadReady(){
            return (this.SquadSize == 5);
        }

        private bool isMissionAssigned(){
            return (this.Mission != null);
        }

    }
}