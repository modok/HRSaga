using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HRSaga.Context.Common.Domain.Model;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads
{
    public class Squad : Entity, IEquatable<Squad>
    {
        public Squad(CaptainId captainId, SquadId squadId, List<ICharacter> members) : base(){  
            this.SquadId = squadId;
            this.CaptainId = captainId;
            this.Members = members; 
        }
        public Squad(CaptainId captainId) : base(){  
            this.SquadId = new SquadId();
            this.CaptainId = captainId;
            this.Members = new List<ICharacter>(); 
        }
        public SquadId SquadId { get; private set; } 
        public CaptainId CaptainId {get; private set;}
        readonly List<ICharacter> Members;

        public bool Equals([AllowNull] Squad other)
        {
            throw new NotImplementedException();
        }

        public int totalMembers(){
            return this.Members.Count;
        }
        public int numMembersOf(CharacterType characterType){
            return this.Members.FindAll(
                character => character.CharacterType == CharacterType.Wizard
                ).Count;
        }

        public  List<ICharacter> MembersOf(CharacterType characterType){
            return this.Members.FindAll(
                character => character.CharacterType == CharacterType.Warrior
                );
        }

        public void addCharacter(ICharacter character){
            AssertionConcern.AssertArgumentNotNull(character,"You can't hire null");
            this.Members.Add(character);
        }

        public bool isReady(){
            return (this.Members.Count ==5);
        
        }
    }
}