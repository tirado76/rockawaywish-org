using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RockawayWish.Web.Models;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Controllers
{
    public class ContactController : BaseController
    {
        [Route("contact-wish")]
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contact-wish")]
        public ActionResult Index(ContactViewModel model)
        {

            return View();
        }
    }
}