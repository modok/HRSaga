using HRSaga.Artefacts;
using HRSaga.Context.Shared;

namespace HRSaga.Context.InTheTavern
{
    public class Captain : Entity
    {
        private CaptainId _captainId;
        private Tavern _tavern;
        private Mission _mission;

        public Captain(CaptainId captainId, Tavern tavern)
        {
            _captainId = captainId; 
            _tavern = tavern;
            CaptainRepository.save(this);
        }
        
        


        public CaptainId getCaptainId(){
            return this._captainId;
        }

        public Tavern getTavern(){
            return this._tavern;
        }

        public bool hasMission(){
            return (this._mission == null?false:true);
        }

        /*
        public List<Mission> LookTheMissionOnTavernBoard()
        {
            Console.WriteLine("The board contains {0} missions!", _tavern.ShowMissionsInTheBoard().Count);
            return _tavern.ShowMissionsInTheBoard();
        }

*/

        public Mission getFirstMissionAvailable(){
           return  _tavern.ShowMissionsInTheBoard()[0];
        }

        public CaptainId acceptMission(Mission mission)
        {
            _tavern = null;
            _mission = mission;
            CaptainRepository.save(this);
            return this.getCaptainId();
        }
/*
        public OverTheRealm.Entities.Captain exitWithoutMission()
        {
            return _tavern.exit(this);
        }
        */
    }

}
