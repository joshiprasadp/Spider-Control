using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using RoboticSpider.Application;
using RoboticSpider.Application.Queries.MoveSpider;
using RoboticSpider.Web.Models;

namespace RoboticSpider.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            // Used mediator to decouple UI.
            _mediator = mediator;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RobotMovesModel model)
        {
            // Check if ModelState is valid as per Entity validation rules.
            if (ModelState.IsValid)
            {
                // Send query to query handler
               var response = await _mediator.Send(new MoveSpiderQuery
                {
                    Moves = model.Moves, CurrentPosition = model.Position, WallGridSize = model.Grid
                });
               // If move is successful then update current position to new position.
               if (response.IsSuccess)
               {
                   // This is not required if we want to just display as a label may be.
                   // In my case I am updating current position with the latest one.
                   ModelState.Clear();
                   model.Position = response.CurrentPositionOfSpider;
               }
               else
               {
                   // For better UX, let user know which input caused this failure.
                   ModelState.AddModelError("Moves", response.ErrorMessage);
               }
            }

            return View(model);
        }
    }
}