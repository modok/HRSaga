using HRSaga.Artefacts;
using HRSaga.Context.Shared;

namespace HRSaga.Context.InMission
{
    public class Captain : Entity
    {
        private CaptainId _captainId;
        private Mission _mission;
        private int _gold;

        public Captain(CaptainId captainId, Mission mission, int gold)
        {
            _captainId = captainId;
            _mission = mission;
            _gold = gold;
            CaptainRepository.save(this);
        }

        public CaptainId getCaptainId(){
            return this._captainId;
        }

        public Mission getMission(){
            return _mission;
        }

        public int getGold(){
            return _gold;
        }

        public CaptainId missionCompleted(){
            _gold = _gold + getMission().GetReward();
            _mission = null;
            CaptainRepository.save(this);
            return this.getCaptainId();
        }

    }
}
