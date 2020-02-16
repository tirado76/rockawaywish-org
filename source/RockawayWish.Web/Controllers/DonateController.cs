using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;

using RockawayWish.Web.Models;

using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Constants;
using InteractiveMembership.Data.Providers;

using CaptchaMvc.HtmlHelpers;


namespace RockawayWish.Web.Controllers
{
    public class DonateController : BaseController
    {
        private UsersProvider _userProvider = new UsersProvider();
        private UserModel _userModel = new UserModel();

        public DonateController()
        {
            _provider = new UserPaymentsProvider();
            _model = new UserPaymentsModel();
        }
        private UserPaymentsProvider _provider;
        private UserPaymentsModel _model;

        [Route("donate")]
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
                return Redirect("~/donate/payment");

            return View(new RegisterViewModel
            {
                YearJoined = DateTime.Now.Year
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("donate")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    ModelState.AddModelError("", "Error: Captcha is not valid");
                    return View(model);
                }

                model.YearJoined = DateTime.Now.Year;

                bool isDonator = true;
                // register user
                var result = await _userProvider.Create(new Guid(Config.ApplicationId), model.Email, Guid.NewGuid().ToString(), model.FirstName, model.LastName, true, false, isDonator, false, false, model.YearJoined, model.Address, model.City, model.State, model.Country, model.Zip, model.Phone, model.CellPhone);

                if (result.Status == 1)
                    Session["DonationUserId"] = new UsersProvider().Get(new Guid(Config.ApplicationId)).Result.Where(x => x.Email.Equals(model.Email)).FirstOrDefault().UserId;

                else
                    Session["DonationUserId"] = result.UserId;
                // send email to membership administrator with registration information
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<p>The following user has registered as a donor on the WISH of Rockaway website and needs to be sent a welcome email.</p>");
                sb.AppendFormat("<p>Registered as: {0}</p>", "Donor");

                sb.AppendFormat("<p>First Name: {0}</p>", model.FirstName);
                sb.AppendFormat("<p>Last Name: {0}</p>", model.LastName);
                sb.AppendFormat("<p>Email: {0}</p>", model.Email);
                sb.AppendFormat("<p>Address: {0}</p>", model.Address);
                sb.AppendFormat("<p>City: {0}</p>", model.City);
                sb.AppendFormat("<p>State: {0}</p>", model.State);
                sb.AppendFormat("<p>Zip: {0}</p>", model.Zip);
                sb.AppendFormat("<p>Country: {0}</p>", model.Country); 
                sb.AppendFormat("<p>Phone: {0}</p>", model.Phone);
                sb.AppendFormat("<p>Cell Phone: {0}</p>", model.CellPhone);
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel to approve the user.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");

                var emailResult = this.SendEmail(this.MembershipAuditEmail, this.MembershipAuditName, "A user has registered as a donor on the WISH of Rockaway website", sb.ToString());

                //// Authenticate user
                //StringBuilder ticketName = new StringBuilder();
                //ticketName.AppendFormat("{0}", string.Format("{0} {1}", result.FirstName, result.LastName));
                //ticketName.AppendFormat("|{0}", result.UserId.ToString());
                //ticketName.AppendFormat("|{0}", result.AccessToken);
                //ticketName.AppendFormat("|{0}", result.Email);
                //ticketName.AppendFormat("|{0}", result.IsActive.ToString());
                //ticketName.AppendFormat("|{0}", result.IsUser.ToString());
                //ticketName.AppendFormat("|{0}", result.IsAdmin.ToString());
                //ticketName.AppendFormat("|{0}", result.IsSuperAdmin.ToString());
                //ticketName.AppendFormat("|{0}", result.IsDonator.ToString());
                //FormsAuthentication.SetAuthCookie(ticketName.ToString(), true);


                return RedirectPermanent("~/donate/payment");
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }


        //[Authorize]
        public ActionResult Payment()
        {
            Session["UserDonationPaymentId"] = this.DonationPaymentId;
            Session["UserPaymentMethod"] = "PayPal";
            Session["UserPaymentType"] = (int)UserPaymentType.Donation; ;



            ViewBag.PayPalDonateButtonId = this.PayPalDonateButtonId;
            return View();
        }
        //[Authorize]
        public ActionResult CancelPayment()
        {
            var user = new UsersProvider().GetById(this.ApplicationId, new Guid(Session["DonationUserId"].ToString())).Result;

            if (user != null && user.Status == 0)
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
        //[Authorize]
        public ActionResult CompletePayment()
        {
            
            // get user info
            var user = new UsersProvider().GetById(this.ApplicationId, new Guid(Session["DonationUserId"].ToString())).Result;
            decimal amount = 50m;
            _model = _provider.Create(this.ApplicationId, new Guid(Session["DonationUserId"].ToString()), new Guid(Session["UserDonationPaymentId"].ToString()), (int)Session["UserPaymentType"], Session["UserPaymentMethod"].ToString(), DateTime.Now, amount).Result;

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
            Session["DonationUserId"] = null;

        }

    }
}