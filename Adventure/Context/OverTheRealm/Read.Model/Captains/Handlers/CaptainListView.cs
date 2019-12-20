using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSCode.ReadModel.Captains.Queries;
using CQRSlite.Events;
using CQRSlite.Queries;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Infrastructure;

namespace HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Handlers
{
    public class CaptainListView : 
        ICancellableEventHandler<CaptainCreated>, 
        ICancellableEventHandler<CharacterHired>,
        ICancellableEventHandler<CaptainSquadFullFilled>,
        ICancellableQueryHandler<GetCaptainList, List<CaptainDto>>,
        ICancellableQueryHandler<GetCaptain,CaptainDto>
    {
        public Task Handle(CaptainCreated message, CancellationToken token = default)
        {
            CaptainDto captain = new CaptainDto(new CaptainId(message.Identity));
            InMemoryDatabase.List.Add(captain);
            InMemoryDatabase.Details.Add(captain.CaptainId,captain);
            return Task.CompletedTask;
        }

        public Task<List<CaptainDto>> Handle(GetCaptainList message, CancellationToken token = default)
        {
            return Task.FromResult(InMemoryDatabase.List);
        }

        public Task Handle(CharacterHired message, CancellationToken token = default)
        {
            //var item = InMemoryDatabase.List.Find(x => x.Id == message.Id);
            var item = InMemoryDatabase.Details.SingleOrDefault(x => x.Key.Equals(message.Identity)).Value;
            
            switch (message.Character.CharacterType)
            {
                case CharacterType.Warrior:
                    item.warriors=item.warriors+1;
                break;    
                case CharacterType.Wizard:
                    item.wizard=item.wizard+1;
                break;
            }
            return Task.CompletedTask;
        }

        public Task Handle(CaptainSquadFullFilled message, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<CaptainDto> Handle(GetCaptain message, CancellationToken token = default)
        {
           return Task.FromResult(InMemoryDatabase.Details.SingleOrDefault(x => x.Key.Equals(message.CaptainId)).Value);
        }

        
    }
}