using System;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockawayWish.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public async Task<ActionResult> Index(string err)
        {
            await Task.Delay(0);
            ViewBag.ErrorMessage = err;
            return View();
        }
        public async Task<ActionResult> PageNotFound()
        {
            await Task.Delay(0);

            return View();
        }
        public async Task<ActionResult> NoAccess()
        {
            await Task.Delay(0);
            Response.StatusCode = 301;
            return View();
        }
    }
}