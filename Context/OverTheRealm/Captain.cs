using System;
using HRSaga.Artefacts;
using HRSaga.Context.Shared;

namespace HRSaga.Context.OverTheRealm
{
    public class Captain : Entity
    {
        private CaptainId _captainId;
        private Squad _squad;
        private Tavern _tavern;
        
        public Captain()
        {
            _captainId = CaptainRepository.nextIdentity();
            _squad = new Squad();
        }

        public Captain(CaptainId captainId, Squad squad )
        {
            _captainId = captainId;
            _squad = squad;
        }

        public void Hire(Warrior warrior){
            _squad.hire(warrior);
            CaptainRepository.save(this);
        }

        public void Hire(Wizard wizard)
        {
            _squad.hire(wizard);
            CaptainRepository.save(this);
        }

        public bool SquadIsReady(){
            return _squad.isTheSquadFull();
        }

        public CaptainId getCaptainId(){
            return _captainId;
        }

        public Squad getSquad(){
            return _squad;
        }

        public bool isInTavern(){
            return (_tavern == null)?false:true;
        }

        public CaptainId goToTavern(Tavern tavern){
            if (!SquadIsReady())
                throw new InvalidOperationException("The squad not ready");
            _tavern=tavern;
            CaptainRepository.save(this);
            return this.getCaptainId();
        }

        /*
        public InTheTavern.Captain goToTavern(Tavern tavern){
            if (!SquadIsReady())
                throw new InvalidOperationException("The squad not ready");
            
            return new InTheTavern.Captain(this._captainId.ToString(),tavern);                
            
        }

        /*
        public Captain(InMission.Entities.Captain captain): base(captain)
        {
            
        }

        public Captain(Context.InTheTavern.Entities.Captain captain) : base(captain)
        {

        }
        public InTheTavern.Entities.Captain goToTavern(Tavern tavern){
            if (!SquadIsReady())
                throw new SquadNotReady();
            
            return new InTheTavern.Entities.Captain(this,tavern);                 
            
        }
        */


    }
}