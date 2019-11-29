using System;
using HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainService
    {
        public CaptainService( ICaptainRepository captainRepository ){
            this.CaptainRepository = captainRepository;
        }

        readonly ICaptainRepository CaptainRepository;

        public CaptainId newCaptain(){
            CaptainId captainId = this.CaptainRepository.GetNextIdentity();
            Captain captain = new Captain(captainId,new Squad(captainId));
            this.CaptainRepository.Save(captain);
            return captain.CaptainId;
        }

        public  void hire(CaptainId captainId, ICharacter character){
            Captain captain = this.CaptainRepository.Get(captainId);
            captain.hire(character);
            this.CaptainRepository.Save(captain);
        }

        public Captain get(CaptainId captainId){
            return this.CaptainRepository.Get(captainId);
        }
    }
}