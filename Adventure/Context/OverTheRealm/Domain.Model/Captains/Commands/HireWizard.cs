using EventFlow.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class HireWizard : Command<Captain, CaptainId>
    {
        public readonly Wizard Wizard;

        
        public HireWizard(CaptainId captainId, Wizard wizard):base(captainId)
        {
            this.Wizard=wizard;
        }
    }
}
 