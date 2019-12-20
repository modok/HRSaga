using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers
{
    public class HireWizardCommandHandlers : 
        CommandHandler<Captain, CaptainId, HireWizard>
    {
        public override Task ExecuteAsync(Captain aggregate, HireWizard command, CancellationToken cancellationToken)
        {
            aggregate.hire(command.Wizard);
            return Task.FromResult(0);
        }
    }
}
