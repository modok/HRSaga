using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HRSaga.Context.Common.Domain.Model;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public class Squad : Entity, IEquatable<Squad>
    {
        /*
            Nota
            L'identificativo della squad e' un GUID rappresentato da squadId
            Se assumiamo che una squad non puo' mai cambiare capitano, al max i membri possono cambiare squadra
            potremmo costruire una chiave composta per definire la Squad. 
            CaptainId + num che oggi potrebbe essere anche una costante.
            Cosi' facendo rafforzerei ulteriormente il legame con il Capitano.. boh da pensarci
        */
        public Squad(CaptainId captainId, SquadId squadId) : base(){  
            this.SquadId = squadId;
            this.CaptainId = captainId;
            this.Recruits = new HashSet<Character>(); 
        }

        public CaptainId CaptainId {get; private set;}
        public SquadId SquadId { get; private set; } 

        public HashSet<Character> Recruits {get; private set;}

        

        public bool Equals([AllowNull] Squad other)
        {
            throw new NotImplementedException();
        }

        public void hire(Character character){
            AssertionConcern.AssertArgumentNotNull(character,"You can't hire null");
            
            Recruits.Add(character);
        }

        public bool isReady(){
            //this.Recruits
            return (this.Recruits.Count ==5);
            
        }
    }
}