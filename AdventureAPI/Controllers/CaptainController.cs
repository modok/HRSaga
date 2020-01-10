using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventFlow.Queries;
using System.Threading;
using EventFlow;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains;
using HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Queries;

namespace AdventureAPI.Controllers
{
    [ApiController]
    public class CaptainController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;
        private readonly ILogger<CaptainController> _logger;

        public CaptainController(ILogger<CaptainController> logger,IQueryProcessor queryProcessor,ICommandBus iCommandBus)
        {
            _logger = logger;
            _queryProcessor = queryProcessor;
            _commandBus = iCommandBus;
        }

        [HttpGet("captain/list")]
        public async Task<CaptainReadModel[]> Captains()
        {
            var captains = await _queryProcessor.ProcessAsync(new GetAllCaptainQuery(), CancellationToken.None).ConfigureAwait(false);
            _logger.LogInformation($"captains: {captains.Count().ToString()}");
            return captains.ToArray();            
        }

        [HttpGet("captain/{id}")]
        public async Task<CaptainReadModel> Captain(String id)
        {
            CaptainId captainId =new CaptainId(id);
            var captainReadModel = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<CaptainReadModel>(captainId), CancellationToken.None)
                        .ConfigureAwait(false);

            
            return captainReadModel;
            
        }

        [HttpPost("captain/create")] 
        public async Task<IActionResult> Create()
        {
            try
            {
                IExecutionResult outcome=await _commandBus.PublishAsync(new CreateCaptain(CaptainId.New), CancellationToken.None)
                        .ConfigureAwait(false);    
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
            return Accepted();
        }

        [HttpPost("captain/{id}/hire/{characterType}")] 
        public async Task<IActionResult> HireWarrior(String id, CharacterType characterType)
        {
            try
            {
                CaptainId captainId =new CaptainId(id);
                Command<Captain, CaptainId> command=null;
                switch(characterType){
                    case CharacterType.Warrior:
                        command = new HireWarrior(captainId,new Warrior());
                    break;
                    case CharacterType.Wizard:
                        command = new HireWizard(captainId,new Wizard());
                    break;
                }
                if(command is null){
                    return BadRequest();    
                }

                await _commandBus.PublishAsync(command, CancellationToken.None)
                        .ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
            return Accepted();
        }



    }
}
