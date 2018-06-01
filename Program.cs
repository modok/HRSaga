using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HRSaga.Entity.Characters;
using HRSaga.Entities;
using System.Collections.Generic;

namespace HRSaga
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger("");

            Console.WriteLine("Hello World!");
            Context.Free.Captain captainFree = new Context.Free.Captain(logger);
            captainFree.hire(new Wizard());
            captainFree.hire(new Warrior());
            captainFree.hire(new Wizard());
            captainFree.hire(new Warrior());
            captainFree.hire(new Wizard());

            Context.Tavern.Captain captainInTavern =  captainFree.goToTavern(new Tavern());
            List<MissionAvailable> missionOntheBoard = captainInTavern.lookTheMissionOnTavernBoard();
            while(missionOntheBoard.Count == 0){
                Console.WriteLine("No Mission");
                Console.WriteLine("No Mission, change Tavern..");
                captainFree = captainInTavern.exitWithoutMission();
                captainInTavern = captainFree.goToTavern(new Tavern());
                missionOntheBoard = captainInTavern.lookTheMissionOnTavernBoard();
            }
            Context.Mission.Captain captainInMission = captainInTavern.acceptMission(missionOntheBoard[0]);
            captainFree = captainInMission.missionCompleted();

            Console.WriteLine("Captain is ready for another mission!");
            Console.WriteLine("The captain has:");
            Console.WriteLine("The squad is ready: {0}", captainFree.squadIsReady());
            Console.WriteLine("The captain has {0} gold", captainFree.gold);
        }
    }
}
