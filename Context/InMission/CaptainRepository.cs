using System.Collections.Generic;
using System.Collections.ObjectModel;
using HRSaga.Artefacts;
using HRSaga.Context.Shared;
using HRSaga.PersistLayer;

namespace HRSaga.Context.InMission
{
    public class CaptainRepository : Repository
    {
        private static bool isInTheRightContext(CaptainPersisted captainPersisted){
            if(captainPersisted.HasMission && !captainPersisted.IsInTheTavern){
                return true;
            }
            return false;
        }
        private static Captain buildCaptain(CaptainId captainId, CaptainPersisted captainPersisted){
            Mission mission = captainPersisted.HasMission? new Mission() : null;
            return new Captain(captainId, mission, captainPersisted.Gold);
        }
        private static Captain buildCaptain(CaptainPersisted captainPersisted){
            return buildCaptain(new CaptainId(captainPersisted.Guid),captainPersisted);
        }

        public static Collection<Captain> allCaptains(){
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

        public static Captain captainOfId(CaptainId captainId){
            SqlLite db = new SqlLite();
            CaptainPersisted captainPersisted = db.getCaptain(captainId.ToString());
            
            if(!isInTheRightContext(captainPersisted)){
                throw new KeyNotFoundException();
            }

            return buildCaptain(captainId,captainPersisted);
        }

        public static void save(Captain captain){
            SqlLite db = new SqlLite();
            db.saveCaptain(captain);
        }

    }
}