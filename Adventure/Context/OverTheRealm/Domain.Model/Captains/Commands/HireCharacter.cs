using System;
using CQRSlite.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class HireCharacter : ICommand{

        public readonly ICharacter Character;

        public HireCharacter(CaptainId captainId, Warrior warrior){
            this.Id=captainId.Id;
            this.Character=warrior;
        }

        public HireCharacter(CaptainId captainId, Wizard wizard){
            this.Id=captainId.Id;
            this.Character=wizard;
        }

        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }
        
    }
}
 