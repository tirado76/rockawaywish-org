using System.Threading.Tasks;
using System.Web.Mvc;

using RockawayWish.Web.ViewModels;


namespace RockawayWish.Web.Controllers
{
    public class GlobalController : Controller
    {
        #region Public Methods
        /// <summary>
        /// Navigation
        /// </summary>
        /// <returns></returns>
        public ActionResult Navigation()
        {
            NavigationVM vm = new NavigationVM();
            return View(vm);
        }

        /// <summary>
        /// Footer
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Footer()
        {
            await Task.Delay(0);

            return View(new FooterVM());
        }

        /// <summary>
        /// Carousel
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Carousel()
        {
            await Task.Delay(0);

            return View(new CarouselVM());
        }
        #endregion

    }
}