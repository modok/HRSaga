using System;
using System.Collections.Generic;
using HRSaga.Context.OverTheRealm.Application;

namespace HRSaga
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Client that call Application OverTheReal
            ApplicationOverTheRealmCaptainService appOverTheRealmCaptainService =new ApplicationOverTheRealmCaptainService();
            
            //I want to create a new captain
            Context.OverTheRealm.Domain.Model.CaptainId captainId = appOverTheRealmCaptainService.commandNewCaptain();
            
            //I want to hire a Warrior
            appOverTheRealmCaptainService.commandHireWarrior(captainId);
            
            //I want to hire a Wizard
            appOverTheRealmCaptainService.commandHireWizard(captainId);

            //I ask if the quad is ready
            Context.OverTheRealm.Domain.Model.Captain captain = appOverTheRealmCaptainService.queryCaptain(captainId);
            while(!captain.isSquadReady()){
                //if not ready, I want to hire new Wizard until the Squad is ready
                appOverTheRealmCaptainService.commandHireWizard(captainId);
                captain = appOverTheRealmCaptainService.queryCaptain(captainId);
            }

            //I want to go to the Tavern
            //appInTheTavernService.commandGoToTavern(captainId, new Tavern());

        }
    }
}
