using System;
using System.Collections.Generic;
using HRSaga.Context.Shared;
using HRSaga.PersistLayer;

namespace HRSaga
{
    class Program
    {
        
        public static void scenario1(){
            
            //I'm over the Real to hire a squad
            Context.OverTheRealm.Captain captainOverTheRealm = new Context.OverTheRealm.Captain();
            captainOverTheRealm.Hire(new Context.OverTheRealm.Wizard());
            captainOverTheRealm.Hire(new Context.OverTheRealm.Warrior());
            captainOverTheRealm.Hire(new Context.OverTheRealm.Wizard());
            captainOverTheRealm.Hire(new Context.OverTheRealm.Warrior());
            captainOverTheRealm.Hire(new Context.OverTheRealm.Wizard());
            
            //I'm going to the Tavern
            Context.OverTheRealm.Application appOverTheRealm = new Context.OverTheRealm.Application();   
            Context.InTheTavern.Captain captainInTheTavern = appOverTheRealm.CaptainGoToTavern(captainOverTheRealm,new Context.OverTheRealm.Tavern());

            //I'm looking at bullettinboard 
            List<Context.InTheTavern.Mission> missions=captainInTheTavern.getTavern().ShowMissionsInTheBoard();
            foreach (var mission in captainInTheTavern.getTavern().ShowMissionsInTheBoard())
            {
                Console.WriteLine("mission: {0}", mission);
            }
            
            //I'm choosing a mission
            Context.InTheTavern.Application appInTheTavern = new Context.InTheTavern.Application();   
            Context.InMission.Captain captainInMission = appInTheTavern.acceptMission(captainInTheTavern,missions[0]);
            
            //I'm finisched my mission
            captainInMission.missionCompleted();
        }
        
        public static void scenario2(){
            SqlLite db = new SqlLite();

            //Loading an old captain
            Context.OverTheRealm.Captain captainOverTheRealm = Context.OverTheRealm.CaptainRepository.allCaptains()[0];
            if(captainOverTheRealm == null){
                throw new ArgumentNullException();
            }
            
            //I'm going to the Tavern
            Context.OverTheRealm.Application appOverTheRealm = new Context.OverTheRealm.Application();   
            Context.InTheTavern.Captain captainInTheTavern = appOverTheRealm.CaptainGoToTavern(captainOverTheRealm,new Context.OverTheRealm.Tavern());

            //I'm looking at bullettinboard 
            List<Context.InTheTavern.Mission> missions=captainInTheTavern.getTavern().ShowMissionsInTheBoard();
            foreach (var mission in captainInTheTavern.getTavern().ShowMissionsInTheBoard())
            {
                Console.WriteLine("mission: {0}", mission);
            }
            
            //I'm choosing a mission
            Context.InTheTavern.Application appInTheTavern = new Context.InTheTavern.Application();   
            Context.InMission.Captain captainInMission = appInTheTavern.acceptMission(captainInTheTavern,missions[0]);
            
            //I'm finisched my mission
            captainInMission.missionCompleted();
        }

        public static void scenario3(){
            //load all captains persisted
             foreach (var item in Context.OverTheRealm.CaptainRepository.allCaptains())
            {
                Console.WriteLine("OverTheRealm captain id: {0}", item.getCaptainId().ToString());
            }

            foreach (var item in Context.InTheTavern.CaptainRepository.allCaptains())
            {
                Console.WriteLine("InTheTavern captain id: {0}", item.getCaptainId().ToString());
            }

            foreach (var item in Context.InMission.CaptainRepository.allCaptains())
            {
                Console.WriteLine("InMission captain id: {0}", item.getCaptainId().ToString());
            }

        }
        static void Main(string[] args)
        {
            SqlLite db = new SqlLite();
            db.initializzateCaptainTable(true); //set false if you don't want to delete the db everytime

            scenario1();
            
            //or
            scenario2();
            
            //or
            scenario3();
        }
    }
}
