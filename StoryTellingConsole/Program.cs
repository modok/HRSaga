using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StoryTellingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

             serviceProvider
            .GetService<ILoggerFactory>()
            .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger("");
            logger.LogInformation("Starting application");

            var story = new Story(logger);
            story.TellMeAboutTheCaptainAndHisFirstMission();

            System.Console.ReadKey();
            logger.LogInformation("Closing application");

        }
    }
}
