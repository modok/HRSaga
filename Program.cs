using System;

namespace HRSaga
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            //Client that call Application OverTheReal
            Context.OverTheRealm.Application.ApplicationCaptainService applicationCaptainService =new Context.OverTheRealm.Application.ApplicationCaptainService();

            //I want to create a new captain
            Context.OverTheRealm.Domain.Model.Captains.CaptainId captainId = applicationCaptainService.commandNewCaptain();
            Console.WriteLine("The new Captain has Id: {0}", captainId);
            captainId = applicationCaptainService.commandNewCaptain();
            Console.WriteLine("The new Captain has Id: {0}", captainId);
            
            //I want to hire a Warrior
            applicationCaptainService.commandHireWarrior(captainId);
            
            //I want to hire a Wizard
            applicationCaptainService.commandHireWizard(captainId);

            //I ask if the quad is ready
            Context.OverTheRealm.Domain.Model.Captains.Captain captain = applicationCaptainService.queryCaptain(captainId);
            Console.WriteLine("The Captain({0}) has the squad({1}) ready: {2}", captainId,captain.Squad.SquadId.Id,captain.isSquadReady());
            while(!captain.isSquadReady()){
                //if not ready, I want to hire new Wizard until the Squad is ready
                applicationCaptainService.commandHireWizard(captainId);
                captain = applicationCaptainService.queryCaptain(captainId);
                Console.WriteLine("The Captain({0}) has the squad({1}) ready: {2}", captainId,captain.Squad.SquadId.Id,captain.isSquadReady());
            }
            
            //I want to go to the Tavern
            //appInTheTavernService.commandGoToTavern(captainId, new Tavern());
            Console.WriteLine("End");
        }
    }
}
