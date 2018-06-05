using System;
using Microsoft.Extensions.DependencyInjection;
using HRSaga.ValueObjects;
using HRSaga.Entities;
using System.Collections.Generic;

namespace HRSaga
{
    class Program
    {
        static void Main(string[] args)
        {

            Context.OverTheRealm.Entities.Captain captainOverTheRealm = new Context.OverTheRealm.Entities.Captain();
            captainOverTheRealm.Hire(new Wizard());
            captainOverTheRealm.Hire(new Warrior());
            captainOverTheRealm.Hire(new Wizard());
            captainOverTheRealm.Hire(new Warrior());
            captainOverTheRealm.Hire(new Wizard());

            Context.InTheTavern.Entities.Captain captainInTavern =  captainOverTheRealm.goToTavern(new Tavern());
            List<Mission> missionOntheBoard = captainInTavern.LookTheMissionOnTavernBoard();
            while(missionOntheBoard.Count == 0){
                Console.WriteLine("No Mission");
                Console.WriteLine("No Mission, change Tavern..");
                captainOverTheRealm = captainInTavern.exitWithoutMission();
                captainInTavern = captainOverTheRealm.goToTavern(new Tavern());
                missionOntheBoard = captainInTavern.LookTheMissionOnTavernBoard();
            }
            Context.InMission.Entities.Captain captainInMission = captainInTavern.acceptMission(missionOntheBoard[0]);
            captainOverTheRealm = captainInMission.MissionCompleted();

            Console.WriteLine("Captain is ready for another mission!");
            Console.WriteLine("The captain has:");
            Console.WriteLine("The squad is ready: {0}", captainOverTheRealm.SquadIsReady());
            Console.WriteLine("The captain has {0} gold", captainOverTheRealm.GetGold());
        }
    }
}
