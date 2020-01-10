using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Events;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Handlers;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.AspNetCore.Extensions;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Queries;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains;

namespace AdventureAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
//            services.AddEventFlow(cfg =>
//                cfg.AddDefaults(typeof(Startup).Assembly)
//                     .AddAspNetCoreMetadataProviders());
            services.AddEventFlow(options => options
            .AddDefaults(typeof(Startup).Assembly)
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
                .AddAspNetCore()
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<CommandPublishMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
