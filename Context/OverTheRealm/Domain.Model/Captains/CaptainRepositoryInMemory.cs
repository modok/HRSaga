using System;
using System.Collections.Generic;
using HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainRepositoryInMemory : ICaptainRepository
    {
        private static CaptainRepositoryInMemory instance=null;
        static readonly object padlock = new object();
        private List<Captain> CaptainsDB=new List<Captain>();      
        private int nextIdentity= 3;

        CaptainRepositoryInMemory(){
            loadCaptainDB();
        }
        public static CaptainRepositoryInMemory Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance==null)
                {
                    instance = new CaptainRepositoryInMemory();
                }
                return instance;
            }
        }
    }
        public Captain Get(CaptainId captainId)
        {
            return this.CaptainsDB.Find(c => c.CaptainId == captainId);
        }

        public ICollection<Captain> GetAllCaptains()
        {
            return this.CaptainsDB;
        }

        public CaptainId GetNextIdentity()
        {
            
            return (new CaptainId(Guid.NewGuid().ToString()));
        }

        public void Remove(CaptainId captainId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAll(IEnumerable<Captain> captains)
        {
            throw new System.NotImplementedException();
        }

        public void Save(Captain captain)
        {
            Captain captainFound = this.CaptainsDB.Find(s => s.CaptainId == captain.CaptainId);
            if(captainFound != null){
                this.CaptainsDB.Remove(captainFound);
            }
            this.CaptainsDB.Add(captain);
        }

        public void SaveAll(IEnumerable<Captain> squads)
        {
            foreach (Captain squad in squads)
            {
                Save(squad);
            }
        }

        private void loadCaptainDB(){
            //Captain1
            CaptainId captainId1 = new CaptainId("1");
            SquadId squadId1 = new SquadId("1");
            List<ICharacter> members1=new List<ICharacter>();
            members1.Add(new Warrior());
            members1.Add(new Warrior());
            members1.Add(new Wizard());
            members1.Add(new Wizard());
            Squad squad1 = new Squad(captainId1,squadId1,members1);
            Captain captain1= new Captain(captainId1,squad1);

            //Captain2
            CaptainId captainId2 = new CaptainId("2");
            SquadId squadId2 = new SquadId("2");
            Squad squad2 = new Squad(captainId2,squadId2,new List<ICharacter>());
            Captain captain2= new Captain(captainId2,squad2);

            this.CaptainsDB.Add(captain1);
            this.CaptainsDB.Add(captain2);
        }

        
    }
}