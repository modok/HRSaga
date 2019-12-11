using System;
using HRSaga.Adventure.Context.Common.Domain.Model;

namespace HRSaga.Adventure.Context.InTavern.Domain.Model.Captains
{
    public class CaptainId : Identity
    {
        public CaptainId(): base()
        {
        }

        public CaptainId(Guid id): base(id)
        {
        }
    }
}
