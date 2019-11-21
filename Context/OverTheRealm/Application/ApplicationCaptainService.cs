using HRSaga.Context.OverTheRealm.Domain.Model;
using HRSaga.Context.OverTheRealm.Domain.Model.Squads;

namespace HRSaga.Context.OverTheRealm.Application
{
    public class ApplicationOverTheRealmCaptainService
    {
        private CaptainService CaptainService;
        private ICaptainRepository CaptainRepository;
        private ISquadRepository SquadRepository;
        public ApplicationOverTheRealmCaptainService(){
            this.CaptainRepository = null;
            this.SquadRepository = null;
            this.CaptainService = new CaptainService(this.CaptainRepository,this.SquadRepository);
        }
        
        public CaptainId commandNewCaptain(){
            Captain captain = this.CaptainService.newCaptain();
            return captain.CaptainId;
        }

        public void commandHireWarrior(CaptainId captainId){
            this.CaptainService.hire(captainId,new Warrior());
        }

        public void commandHireWizard(CaptainId captainId){
            this.CaptainService.hire(captainId,new Wizard());
        }

        public Captain queryCaptain(CaptainId captainId){
            return this.CaptainService.get(captainId);
        }


    }
}