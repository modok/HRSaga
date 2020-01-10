using System.Collections.Generic;
using System.Threading;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Queries;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Queries;

namespace HRSaga.Console
{

        public class TestQuery : IQuery<IReadOnlyCollection<CaptainReadModel>> { }

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(CaptainCreated))
                .AddEvents(typeof(WizardHired))
                .AddEvents(typeof(WarriorHired))
                .AddCommands(typeof(CreateCaptain))
                .AddCommands(typeof(HireWarrior))
                .AddCommands(typeof(HireWizard))
                .AddCommandHandlers(typeof(CreateCaptainCommandHandlers))
                .AddCommandHandlers(typeof(HireWarriorCommandHandlers))
                .AddCommandHandlers(typeof(HireWizardCommandHandlers))
                .AddQueryHandlers(typeof(GetAllCaptainsQueryHandler))
                .UseInMemoryReadStoreFor<CaptainReadModel>()
                
                .CreateResolver())
                {
                
                    CaptainId captainId = CaptainId.New;
                    // Resolve the command bus and use it to publish a command
                    var commandBus = resolver.Resolve<ICommandBus>();
                    await commandBus.PublishAsync(new CreateCaptain(captainId), CancellationToken.None)
                        .ConfigureAwait(false);

                    // Resolve the query handler and use the built-in query for fetching
                    // read models by identity to get our read model representing the
                    // state of our aggregate root
                    var queryProcessor = resolver.Resolve<IQueryProcessor>();
                    var captainReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CaptainReadModel>(captainId), CancellationToken.None)
                        .ConfigureAwait(false);

                    // Verify that the read model has the expected magic number
                    //System.Console.WriteLine($"warriors: {captainReadModel.warriors} wizzards: {captainReadModel.wizards}");

                    await commandBus.PublishAsync(new HireWarrior(captainId,new Warrior()), CancellationToken.None)
                        .ConfigureAwait(false);
                    await commandBus.PublishAsync(new HireWarrior(captainId,new Warrior()), CancellationToken.None)
                        .ConfigureAwait(false);
                    await commandBus.PublishAsync(new HireWizard(captainId,new Wizard()), CancellationToken.None)
                        .ConfigureAwait(false);
                    
                    captainReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<CaptainReadModel>(captainId), CancellationToken.None)
                        .ConfigureAwait(false);
                    
                    System.Console.WriteLine($"warriors: {captainReadModel.warriors} wizzards: {captainReadModel.wizards}");

                    // Resolve the command bus and use it to publish a command
                    await commandBus.PublishAsync(new CreateCaptain(CaptainId.New), CancellationToken.None)
                        .ConfigureAwait(false);

                    captainId  = CaptainId.New;
                    await commandBus.PublishAsync(new CreateCaptain(captainId), CancellationToken.None)
                        .ConfigureAwait(false);

                    await commandBus.PublishAsync(new HireWizard(captainId,new Wizard()), CancellationToken.None)
                        .ConfigureAwait(false);
                    
                    var captains = await queryProcessor.ProcessAsync(new GetAllCaptainQuery(), CancellationToken.None).ConfigureAwait(false);

                    System.Console.WriteLine("------------------------");
                    foreach (var captain in captains)
                    {
                        System.Console.WriteLine($"ID: {captain.captainId}, warr: {captain.warriors}, wiz: {captain.wizards}");
                    }                   
                    System.Console.WriteLine("------------------------");
                }
        }

    }
}
