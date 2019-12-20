using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSCode.ReadModel.Captains.Queries;
using CQRSlite.Caching;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;
using CQRSlite.Queries;
using CQRSlite.Routing;
using HRSaga.Adventure.Common.EventStore.Clients;
using HRSaga.Adventure.Common.EventStore.EventStores;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers;
using Microsoft.Extensions.DependencyInjection;


namespace HRSaga.Console
{

    class Program
    {
        private ServiceProvider Service;
        void startUp()
        {

            var collection = new ServiceCollection();

            collection.AddMemoryCache();

            collection.AddSingleton<Router>(new Router());
            collection.AddSingleton<ICommandSender>(y => y.GetService<Router>());
            collection.AddSingleton<IEventPublisher>(y => y.GetService<Router>());
            collection.AddSingleton<IHandlerRegistrar>(y => y.GetService<Router>());
            collection.AddSingleton<IQueryProcessor>(y => y.GetService<Router>());
            collection.AddSingleton<IEventStore>(y => new EVEventStore(y.GetService<IEventPublisher>(),new EVClient(),"HRSAGA")); 
            collection.AddSingleton<ICache, MemoryCache>();
            collection.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            collection.AddScoped<ISession, Session>();

            collection.Scan(scan => scan
                .FromAssemblies(typeof(CaptainCommandHandlers).GetTypeInfo().Assembly)
                    .AddClasses(classes => classes.Where(x =>
                    {
                        var allInterfaces = x.GetInterfaces();
                        return
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQueryHandler<,>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableQueryHandler<,>));
                    }))
                    .AsSelf()
                    .WithTransientLifetime()
            );


            this.Service = collection.BuildServiceProvider();
            var registrar = new RouteRegistrar(this.Service);
            registrar.RegisterInAssemblyOf(typeof(CaptainCommandHandlers));


        }

        static void Main(string[] args)
        {

            //EVClient eVClient=new EVClient();
            //eVClient.AddEvent("stream_prova");

            //StackExchange.Redis
            //ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("localhost");

            Program program = new Program();

            var commandSender = program.Service.GetService<ICommandSender>();
            var queryProcessor = program.Service.GetService<IQueryProcessor>();
            CaptainRequest captainRequest = new CaptainRequest(commandSender, queryProcessor);
            captainRequest.newCaptain();
            //captainRequest.list();


        }

        public class CaptainRequest
        {
            private readonly ICommandSender CommandSender;
            private readonly IQueryProcessor QueryProcessor;

            public CaptainRequest(ICommandSender commandSender, IQueryProcessor queryProcessor)
            {

                this.CommandSender = commandSender;
                this.QueryProcessor = queryProcessor;
            }

            public void newCaptain()
            {
                this.CommandSender.Send(new CreateCaptain(new CaptainId()));
            }

            public async void list()
            {
                List<CaptainDto> list = await this.QueryProcessor.Query(new GetCaptainList());
                foreach (var captain in list)
                {
                    System.Console.WriteLine(captain.CaptainId);
                }
            }

            public async void Get(String identity)
            {
                CaptainDto captain = await this.QueryProcessor.Query(new GetCaptain(new CaptainId(identity)));
                System.Console.WriteLine("id: {0},num war: {1},num wiz: {2},", captain.CaptainId, captain.warriors, captain.wizard);
            }


        }

    }
}
