using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Constants;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Controllers
{
    public class DonateController : BaseController
    {
        public DonateController()
        {
            _provider = new UserPaymentsProvider();
            _model = new UserPaymentsModel();
        }
        private UserPaymentsProvider _provider;
        private UserPaymentsModel _model;

        [Authorize]
        [Route("donate")]
        public ActionResult Payment()
        {
            Session["UserDonationPaymentId"] = this.DonationPaymentId;
            Session["UserPaymentMethod"] = "PayPal";
            Session["UserPaymentType"] = (int)UserPaymentType.Donation; ;

            

            ViewBag.PayPalDonateButtonId = this.PayPalDonateButtonId;
            return View();
        }
        [Authorize]
        public ActionResult CancelPayment()
        {
            var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            if (user.Status == 0)
            {
                // send user confirmation email
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>We apologize your the cancellation of your donation.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", UserPaymentType.Donation.ToString());
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                //sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by dropping a comment <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailUserResult = this.SendEmail(user.Email, user.FullName, "Your payment has been canceled", sb.ToString());

                // send membership admin email with user payment
                sb = new StringBuilder();
                sb.AppendLine("<p>The following user has canceled the following donation.</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", UserPaymentType.Donation.ToString());
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                //sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailAdminResult = this.SendEmail(this.MembershipAuditEmail, this.MembershipAuditName, "A donation has been canceled on the WISH of Rockaway website", sb.ToString());

            }
            else
            {

            }
            KillOrderSession();
            return View();
        }
        [Authorize]
        public ActionResult CompletePayment()
        {
            // get user info
            var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;
            decimal amount = 50m;
            _model = _provider.Create(this.ApplicationId, this.UserId, new Guid(Session["UserDonationPaymentId"].ToString()), (int)Session["UserPaymentType"], Session["UserPaymentMethod"].ToString(), DateTime.Now, amount).Result;

            if (_model.Status == 0)
            {
                // send user confirmation email
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Thank you for your donation. Your donation is used to help people in need in the Rockaway community.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Payment: Donation</p>");
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                //sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by writing a message <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>The Wish of Rockaway</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailUserResult = this.SendEmail(user.Email, user.FullName, "Thank you for your donation to WISH of Rockaway", sb.ToString());

                // send membership admin email with user payment
                sb = new StringBuilder();
                sb.AppendLine("<p>The following user has made the following donation.</p>");
                sb.AppendLine("<p>Payment: Donation</p>");
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                //sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailAdminResult = this.SendEmail(this.MembershipAuditEmail, this.MembershipAuditName, "A donation has been made on the WISH of Rockaway website", sb.ToString());

            }
            else
            {

            }

            KillOrderSession();
            return View();
        }
               private void KillOrderSession()
        {
            Session["UserDonationPaymentId"] = null;
            Session["UserPaymentMethod"] = null;
            Session["UserPaymentType"] = null;

        }

    }
}