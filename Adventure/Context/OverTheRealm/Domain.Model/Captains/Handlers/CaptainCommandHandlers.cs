using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers
{
    public class CaptainCommandHandlers : 
        ICommandHandler<CreateCaptain>, 
        ICommandHandler<HireCharacter>
    {
        private readonly ISession _session;

        public CaptainCommandHandlers(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateCaptain message)
        {
            var item = new Captain((CaptainId)message.Identity);
            await _session.Add(item);
            await _session.Commit();
        }

        public async Task Handle(HireCharacter message)
        {
            var item= await _session.Get<Captain>(message.Identity);
            item.hire(message.Character);
            await _session.Commit();
            
        }
    }
}
