using HRSaga.Context.Common.Domain.Model;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains
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
