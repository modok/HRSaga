using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers
{
    public class HireWarriorCommandHandlers : 
        CommandHandler<Captain, CaptainId, HireWarrior>
    {
        public override Task ExecuteAsync(Captain aggregate, HireWarrior command, CancellationToken cancellationToken)
        {
            aggregate.hire(command.Warrior);
            return Task.FromResult(0);
        }
    }
}
