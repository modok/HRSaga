using System;
using CQRSlite.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class CreateCaptain : ICommand{

        public CreateCaptain(CaptainId captainId){
            this.Id=captainId.Id;
        }

        public Guid Id { get; set; }
        public int ExpectedVersion { get; set; }
        
    }
}
 