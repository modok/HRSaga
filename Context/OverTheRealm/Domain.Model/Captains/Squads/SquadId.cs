using System;
using HRSaga.Context.Common.Domain.Model;


namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads
{
    public class SquadId : Identity
    {
        public SquadId(): this(Guid.NewGuid().ToString().ToUpper().Substring(0, 8))
        {
           
        }

        public SquadId(string id): base(id)
        {

        }
    }
}
