using System.Threading.Tasks;
using System.Web.Mvc;

using RockawayWish.Web.ViewModels;


namespace RockawayWish.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Public Methods
        /// <summary>
        ///  Home Page
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            await Task.Delay(0);

            return View(new HomeVM());
        }

        /// <summary>
        /// About Page
        /// </summary>
        /// <returns></returns>
        [Route("about")]
        public async Task<ActionResult> About()
        {
            await Task.Delay(0);

            return View(new AboutVM());
        }

        #endregion
    }
}