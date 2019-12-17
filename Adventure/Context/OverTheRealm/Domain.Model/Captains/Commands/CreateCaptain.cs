using System;
using CQRSlite.Commands;
using CQRSlite.Domain;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class CreateCaptain : ICommand{

        public CreateCaptain(CaptainId captainId){
            this.Identity=captainId;
        }

        public Identity Identity { get; set; }
        public int ExpectedVersion { get; set; }
        
    }
}
 