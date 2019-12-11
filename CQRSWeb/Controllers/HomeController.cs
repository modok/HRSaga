using CQRSlite.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CQRSlite.Queries;
using CQRSCode.ReadModel.Captains.Queries;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains.Commands;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;
using Microsoft.AspNetCore.Routing;

namespace CQRSWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly IQueryProcessor _queryProcessor;

        public HomeController(ICommandSender commandSender, IQueryProcessor queryProcessor)
        {
            _commandSender = commandSender;
            _queryProcessor = queryProcessor;
        }

        public async Task<ActionResult> Index()
        {
            ViewData.Model = await _queryProcessor.Query(new GetCaptainList());

            return View();
        }

        public async Task<ActionResult> Add()
        {
            await _commandSender.Send(new CreateCaptain(new CaptainId()));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(Guid id)
        {
            ViewData.Model = await _queryProcessor.Query(new GetCaptain(id));
            return View();
        }
        public async Task<ActionResult> HireWarrior(Guid id)
        {
            await _commandSender.Send(new HireCharacter(new CaptainId(id),new Warrior()));
            return RedirectToAction("Details",new RouteValueDictionary( 
                new { controller = "Home", action = "Details", id = id } ));
        }
        public async Task<ActionResult> HireWizard(Guid id)
        {
            await _commandSender.Send(new HireCharacter(new CaptainId(id),new Wizard()));
            return RedirectToAction("Details",new RouteValueDictionary( 
                new { controller = "Home", action = "Details", id = id } ));
        }

        /*[HttpPost]
        public async Task<ActionResult> Add(string name, CancellationToken cancellationToken)
        {
            await _commandSender.Send(new CreateInventoryItem(Guid.NewGuid(), name), cancellationToken);
            return RedirectToAction("Index");
        }*/

        
    }
}
