using HRSaga.Artefacts;

namespace HRSaga.Context.InTheTavern
{
    public class Mission : ValueObject
    {
        private int reward = 10;

        public Mission()
        {
        }

        public int GetReward()
        {
            return reward;
        }
    }
}
