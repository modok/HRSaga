using System;
using HRSaga.Context.OverTheRealm.Domain.Model.Captains;
using HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads;

namespace HRSaga.Context.OverTheRealm.Application
{
    public class ApplicationCaptainService
    {
        private CaptainService CaptainService;
        public ApplicationCaptainService(){
            this.CaptainService = new CaptainService(CaptainRepositoryInMemory.Instance);
        }
        
        public CaptainId commandNewCaptain(){

            return this.CaptainService.newCaptain();
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