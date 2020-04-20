using System.Web.Mvc;

namespace RockawayWish.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Public ActionResults
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}