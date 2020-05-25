using System.Web.Mvc;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : AsyncController
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}