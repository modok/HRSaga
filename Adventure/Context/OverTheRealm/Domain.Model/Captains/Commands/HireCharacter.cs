using System;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class HireCharacter : ICommand{

        public Identity Identity { get; set; }
        public int ExpectedVersion { get; set; }
        public readonly ICharacter Character;

        public HireCharacter(CaptainId captainId, Warrior warrior){
            this.Identity=captainId;
            this.Character=warrior;
        }

        public HireCharacter(CaptainId captainId, Wizard wizard){
            this.Identity=captainId;
            this.Character=wizard;
        }

        
        
    }
}
 