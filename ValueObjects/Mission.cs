using System;
namespace HRSaga.ValueObjects
{
    public class Mission
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
