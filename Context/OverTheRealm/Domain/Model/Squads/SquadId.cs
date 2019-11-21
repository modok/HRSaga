using HRSaga.Context.Common.Domain.Model;


namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public class SquadId : Identity
    {
        public SquadId(): base()
        {
        }

        public SquadId(string id): base(id)
        {
        }
    }
}
