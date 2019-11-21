using HRSaga.Context.OverTheRealm.Domain.Model.Squads;

namespace HRSaga.Context.OverTheRealm.Domain.Model
{
    public class CaptainService
    {
        public CaptainService( 
            ICaptainRepository captainRepository,ISquadRepository squadRepository
        ){
            this.CaptainRepository = captainRepository;
            this.SquadRepository = squadRepository;
        }

        readonly ICaptainRepository CaptainRepository;
        readonly ISquadRepository SquadRepository;

        public Captain newCaptain(){
            SquadId squadId = this.SquadRepository.GetNextIdentity();
            CaptainId captainId = this.CaptainRepository.GetNextIdentity();
            Squad squad = new Squad(captainId,squadId);
            Captain captain = new Captain(captainId,squad);
            this.CaptainRepository.Save(captain);
            this.SquadRepository.Save(squad);
            return captain;
        }

        public void hire(CaptainId captainId,Warrior warrior){
           hire(captainId,warrior);
        }
        public void hire(CaptainId captainId,Wizard wizard){
           hire(captainId,wizard);
        }
        private  void hire(CaptainId captainId,Character character){
            Captain captain = this.CaptainRepository.Get(captainId);
            Squad squad=this.SquadRepository.Get(captain.Squad.SquadId);
            squad.hire(character);
            this.SquadRepository.Save(squad);
        }

        public Captain get(CaptainId captainId){
            return this.CaptainRepository.Get(captainId);
        }

    }
}