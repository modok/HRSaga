using EventFlow.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class CreateCaptain : Command<Captain, CaptainId>
    {

        public CreateCaptain(CaptainId captainId): base(captainId)
        {
        }
        
    }
}
 