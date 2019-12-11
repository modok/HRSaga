using System;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainId
    {
        public Guid Id { get; private set; }

        public CaptainId()
        {
            this.Id=Guid.NewGuid();
        }
        public CaptainId(Guid id)
        {
            this.Id=id;
        }
    }
}
