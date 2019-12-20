using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers
{
    public class CreateCaptainCommandHandlers : 
        CommandHandler<Captain, CaptainId, CreateCaptain>
    {
        public override Task ExecuteAsync(Captain aggregate, CreateCaptain command, CancellationToken cancellationToken)
        {
             aggregate.initCaptain();
            return Task.FromResult(0);
        }
    }
}
