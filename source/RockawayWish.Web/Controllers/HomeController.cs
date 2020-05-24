using System.Web.Mvc;


namespace RockawayWish.Web.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            //var homePageVM = new  _SiteVM.HomePageVM;
            // Stop Caching in IE
            //HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            //HttpContext.Response.Cache.SetNoStore();

            // get set site model
            //var siteModel = new SiteRepository().Get().ConfigureAwait(false);

            return View();
        }


    }
}