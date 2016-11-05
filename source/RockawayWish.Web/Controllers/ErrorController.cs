using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockawayWish.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string err)
        {
            return View();
        }
        public ActionResult PageNotFound()
        {

            return View();
        }
        public ActionResult NoAccess()
        {
            Response.StatusCode = 301;
            return View();
        }
    }
}