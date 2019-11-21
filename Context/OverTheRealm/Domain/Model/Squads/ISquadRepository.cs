using System.Collections.Generic;
using HRSaga.Context.OverTheRealm.Domain.Model;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public interface ISquadRepository
    {
        Squad Get(SquadId squadId);

        ICollection<Squad> GetAllSquads();

        SquadId GetNextIdentity();

        void Remove(SquadId squadId);

        void RemoveAll(IEnumerable<Squad> squads);

        void Save(Squad squad);

        void SaveAll(IEnumerable<Squad> squads);

    }
        
}