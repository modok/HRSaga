using HRSaga.Context.Common.Domain.Model;

namespace HRSaga.Context.OverTheRealm.Domain.Model
{
    public class CaptainId : Identity
    {
        public CaptainId(): base()
        {
        }

        public CaptainId(string id): base(id)
        {
        }
    }
}
