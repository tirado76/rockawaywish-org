using System;
using System.Text;
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

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contact-wish")]
        public ActionResult Index(ContactViewModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<p>The following user has submitted a question on the WISH of Rockaway website.</p>");
            sb.AppendFormat("<p>Name: {0}</p>", model.Name);
            sb.AppendFormat("<p>Email: {0}</p>", model.Email);
            sb.AppendFormat("<p>Message: {0}</p>", model.Message);
            sb.AppendLine("<p>&nbsp;</p>");
            sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
            sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
            var emailResult = this.SendEmail(this.ContactUsEmail, "WISH of Rockaway", "Question submitted on the WISH of Rockaway website", sb.ToString());

            if (emailResult.Status == 0)
                return RedirectPermanent("~/contact-wish/confirmation");
            else
                ModelState.AddModelError("", emailResult.Message);

            return View(model);
        }
        [Route("contact-wish/confirmation")]
        public ActionResult Confirmation()
        {

            return View();
        }
    }
}