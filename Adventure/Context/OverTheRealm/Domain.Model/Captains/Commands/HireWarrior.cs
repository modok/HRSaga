using EventFlow.Commands;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands
{
    public class HireWarrior : Command<Captain, CaptainId>
    {
        public readonly Warrior Warrior;

        public HireWarrior(CaptainId captainId, Warrior warrior):base(captainId)
        {
            this.Warrior=warrior;
        }
    }
}
 