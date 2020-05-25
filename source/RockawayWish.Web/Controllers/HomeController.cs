using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebSockets;
using InteractiveMembership.Core.ViewModels;


namespace RockawayWish.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            await Task.Delay(0);

            var homeVM = new HomeVM();
            return View(homeVM);
        }


    }
}