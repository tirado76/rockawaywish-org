using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockawayWish.Web.Controllers
{
    public class DonateController : BaseController
    {
        [Authorize]
        [Route("donate")]
        public ActionResult Payment()
        {
            ViewBag.PayPalDonateButtonId = this.PayPalDonateButtonId;
            return View();
        }
        [Authorize]
        public ActionResult CancelPayment()
        {
            //var dues = new DuesProvider().GetById(this.ApplicationId, new Guid(Session["UserPaymentId"].ToString())).Result;
            //// get user info
            //var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            //if (user.Status == 0 && dues.Status == 0)
            //{
            //    // send user confirmation email
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>We apologize your the cancellation of your payment.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
            //    sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
            //    sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
            //    sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
            //    sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by dropping a comment <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>Regards</p>");
            //    sb.AppendLine("<p>Wish of Rockaway Membership</p>");
            //    sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
            //    var emailUserResult = this.SendEmail(user.Email, user.FullName, "Your payment has been canceled", sb.ToString());

            //    // send membership admin email with user payment
            //    sb = new StringBuilder();
            //    sb.AppendLine("<p>The following user has canceled the following payment.</p>");
            //    sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
            //    sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
            //    sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
            //    sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
            //    sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>Regards</p>");
            //    sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
            //    sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
            //    var emailAdminResult = this.SendEmail(this.MembershipAdminEmail, this.MembershipAdminName, "A payment has been canceled on the WISH of Rockaway website", sb.ToString());

            //}
            //else
            //{

            //}

            //KillOrderSession();
            return View();
        }
        [Authorize]
        public ActionResult CompletePayment()
        {
            //var dues = new DuesProvider().GetById(this.ApplicationId, new Guid(Session["UserPaymentId"].ToString())).Result;
            //// get user info
            //var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            //_model = _provider.Create(this.ApplicationId, this.UserId, new Guid(Session["UserPaymentId"].ToString()), (int)Session["UserPaymentType"], Session["UserPaymentMethod"].ToString(), DateTime.Now, dues.Amount).Result;

            //if (_model.Status == 0)
            //{
            //    // send user confirmation email
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>Thank you for your payment. Your annual membership dues are used to help people in need in the Rockaway community.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
            //    sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
            //    sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
            //    sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by writing a message <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>Regards</p>");
            //    sb.AppendLine("<p>The Wish of Rockaway Membership Team</p>");
            //    sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
            //    var emailUserResult = this.SendEmail(user.Email, user.FullName, "Thank you for your payment", sb.ToString());

            //    // send membership admin email with user payment
            //    sb = new StringBuilder();
            //    sb.AppendLine("<p>The following user has made the following payment.</p>");
            //    sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
            //    sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
            //    sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
            //    sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
            //    sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
            //    sb.AppendLine("<p>&nbsp;</p>");
            //    sb.AppendLine("<p>Regards</p>");
            //    sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
            //    sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
            //    var emailAdminResult = this.SendEmail(this.MembershipAdminEmail, this.MembershipAdminName, "A payment has been made on the WISH of Rockaway website", sb.ToString());

            //}
            //else
            //{

            //}

            //KillOrderSession();
            return View();
        }


    }
}