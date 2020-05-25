using System.Web.Mvc;

using InteractiveMembership.Core.ViewModels;


namespace RockawayWish.Web.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            return View(new HomeVM());
        }


    }
}