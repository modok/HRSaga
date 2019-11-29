using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HRSaga.Context.Common.Domain.Model;
using HRSaga.PersistLayer;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains
{
    public interface ICaptainRepository
    {
        Captain Get(CaptainId captainId);

        ICollection<Captain> GetAllCaptains();

        CaptainId GetNextIdentity();

        void Remove(CaptainId captainId);

        void RemoveAll(IEnumerable<Captain> captains);

        void Save(Captain captain);

        void SaveAll(IEnumerable<Captain> captains);

        


        /* private  bool isInTheRightContext(CaptainPersisted captainPersisted){
            if(!captainPersisted.HasMission && !captainPersisted.IsInTheTavern){
                return true;
            }
            return false;
        }

        private  Captain buildCaptain(CaptainId captainId, CaptainPersisted captainPersisted){
            Squad squad = new Squad(captainPersisted.Num_warrior,captainPersisted.Num_wizzard);
            return new Captain(captainId,squad);
        }
        private  Captain buildCaptain(CaptainPersisted captainPersisted){
            return buildCaptain(new CaptainId(captainPersisted.Guid),captainPersisted);
        }
        
        public  Collection<Captain> allCaptains(){
            Collection<Captain> captains=new Collection<Captain>();
            SqlLite db = new SqlLite();
            foreach (var captainPersisted in db.getCaptains())
            {
                if(isInTheRightContext(captainPersisted)){
                    captains.Add(buildCaptain(captainPersisted));
                }
            }
            return captains;
        }

        protected CaptainId nextIdentity(){
            CaptainId captainId = new CaptainId(Guid.NewGuid().ToString());
            SqlLite db = new SqlLite();
            db.newCaptain(captainId);
            return captainId;
        }

        public  Captain captainOfId(CaptainId captainId){
            SqlLite db = new SqlLite();
            CaptainPersisted captainPersisted = db.getCaptain(captainId.ToString());
            if(!isInTheRightContext(captainPersisted)){
                throw new KeyNotFoundException();
            }
            return buildCaptain(captainId,captainPersisted);
        }

        public  void save(Captain captain){
            SqlLite db = new SqlLite();
            db.saveCaptain(captain);
        }
 */
        
    }
}