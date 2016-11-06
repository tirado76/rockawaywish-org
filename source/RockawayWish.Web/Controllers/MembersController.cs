using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Constants;
using InteractiveMembership.Data.Providers;

using RockawayWish.Web.Models;

namespace RockawayWish.Web.Controllers
{
    [Authorize]
    public class MembersController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // validate user is acitve and is user
            if (!this.UserIsActive || !this.UserIsUser)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "action", "NoAccess" },
                    { "controller", "Error" }
                });
            }
        }
        
        public MembersController()
        {
            _provider = new UserPaymentsProvider();
            _model = new UserPaymentsModel();
        }
        private UserPaymentsProvider _provider;
        private UserPaymentsModel _model;

        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            UserModel model = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            return View(model);

        }
        public ActionResult Payments()
        {
            UserPaymentsViewModel vm = new UserPaymentsViewModel();
            vm.UserId = this.UserId;
            vm.ApplicationId = new Guid(Config.ApplicationId);

            // get user dues list
            List<UserDuesModel> userDuesList = (List<UserDuesModel>)new UserDuesProvider().Get(vm.ApplicationId).Result.Where(x => x.UserId.Equals(vm.UserId)).OrderByDescending(x => x.DuesModel.Title).ToList();

            // get user payments list
            List<UserPaymentsModel> lsUserPayments = (List<UserPaymentsModel>)_provider.Get(vm.ApplicationId).Result.Where(x => x.UserId == vm.UserId).ToList();


            // loop through user dues list and generate dictionary
            foreach (var userDues in userDuesList)
            {
                // get payments for dues
                List<UserPaymentsModel> lsUserPaymentList = (from payment in lsUserPayments
                                                             where payment.UserId == vm.UserId && payment.PaymentId == userDues.DuesId && payment.PaymentTypeId == 1
                                                             select payment).ToList();

                foreach (var userPayment in lsUserPaymentList)
                {
                    userPayment.PaymentDate = userPayment.PaymentDate.Date;
                }
                vm.DuesPaymentList.Add(string.Format("{0}|{1}", userDues.DuesModel.Title, userDues.DuesModel.DuesId), lsUserPaymentList);
            }

            if (userDuesList != null && userDuesList.Count > 0)
                vm.UserModel = userDuesList.FirstOrDefault().UserModel;
            else
                vm.UserModel = new UsersProvider().GetById(vm.ApplicationId, vm.UserId).Result;

            

            return View(vm);
        }

        public ActionResult Payment(Guid paymentId)
        {
            Session["UserPaymentId"] = paymentId;
            Session["UserPaymentMethod"] = "PayPal";
            Session["UserPaymentType"] = 1;

            UserPaymentsViewModel vm = new UserPaymentsViewModel();
            vm.UserId = this.UserId;
            vm.ApplicationId = this.ApplicationId;

            // get user dues list

            List<UserDuesModel> userDuesList = new List<UserDuesModel>();

            UserDuesModel useModel = new UserDuesProvider().GetById(vm.ApplicationId, vm.UserId, paymentId).Result;
            userDuesList.Add(useModel);
            
            // get user payments list
            List<UserPaymentsModel> lsUserPayments = (List<UserPaymentsModel>)_provider.Get(vm.ApplicationId).Result.Where(x => x.UserId == vm.UserId).ToList();


            // loop through user dues list and generate dictionary
            foreach (var userDues in userDuesList)
            {
                // get payments for dues
                List<UserPaymentsModel> lsUserPaymentList = (from payment in lsUserPayments
                                                             where payment.UserId == vm.UserId && payment.PaymentId == userDues.DuesId && payment.PaymentTypeId == 1
                                                             select payment).ToList();

                foreach (var userPayment in lsUserPaymentList)
                {
                    userPayment.PaymentDate = userPayment.PaymentDate.Date;
                }
                vm.DuesPaymentList.Add(string.Format("{0}|{1}|{2}", userDues.DuesModel.Title, userDues.DuesModel.DuesId, userDues.DuesModel.PaypalButtonId), lsUserPaymentList);

            }


            if (userDuesList != null && userDuesList.Count > 0)
                vm.UserModel = userDuesList.FirstOrDefault().UserModel;
            else
                vm.UserModel = new UsersProvider().GetById(vm.ApplicationId, vm.UserId).Result;


            return View(vm);
        }
        public ActionResult CancelPayment()
        {
            var dues = new DuesProvider().GetById(this.ApplicationId, new Guid(Session["UserPaymentId"].ToString())).Result;
            // get user info
            var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            if (user.Status == 0 && dues.Status == 0)
            {
                // send user confirmation email
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
                sb.AppendLine("<p>We apologize for the cancellation of your payment.</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by dropping a comment <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailUserResult = this.SendEmail(user.Email, user.FullName, "Your payment has been canceled", sb.ToString());

                // send membership admin email with user payment
                sb = new StringBuilder();
                sb.AppendLine("<p>The following user has canceled the following payment.</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailAdminResult = this.SendEmail(this.MembershipAuditEmail, this.MembershipAuditName, "A payment has been canceled on the WISH of Rockaway website", sb.ToString());

            }
            else
            {

            }

            KillOrderSession();
            return View();
        }
        public ActionResult CompletePayment()
        {
            var dues = new DuesProvider().GetById(this.ApplicationId, new Guid(Session["UserPaymentId"].ToString())).Result;
            // get user info
            var user = new UsersProvider().GetById(this.ApplicationId, this.UserId).Result;

            _model = _provider.Create(this.ApplicationId, this.UserId, new Guid(Session["UserPaymentId"].ToString()), (int)Session["UserPaymentType"], Session["UserPaymentMethod"].ToString(), DateTime.Now, dues.Amount).Result;

            if (_model.Status == 0)
            {
                // send user confirmation email
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<p>Dear {0},</p>", user.FirstName);
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Thank you for your payment. Your annual membership dues are used to help people in need in the Rockaway community.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.ContactUsEmail + "\">" + this.ContactUsEmail + "</a> or by writing a message <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>The Wish of Rockaway Membership Team</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailUserResult = this.SendEmail(user.Email, user.FullName, "Thank you for your payment", sb.ToString());

                // send membership admin email with user payment
                sb = new StringBuilder();
                sb.AppendLine("<p>The following user has made the following payment.</p>");
                sb.AppendFormat("<p>Payment: {0}</p>", dues.Title);
                sb.AppendFormat("<p>Date: {0}</p>", DateTime.Now.ToShortDateString());
                sb.AppendFormat("<p>Name: {0}</p>", user.FullName);
                sb.AppendFormat("<p>Payment Method: {0}</p>", Session["UserPaymentMethod"].ToString());
                sb.AppendFormat("<p>Amount: {0}</p>", dues.Amount.ToString());
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl + "\">Click here</a> to go to the admin panel.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards</p>");
                sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var emailAdminResult = this.SendEmail(this.MembershipAuditEmail, this.MembershipAuditName, "A payment has been made on the WISH of Rockaway website", sb.ToString());

            }
            else
            {

            }

            KillOrderSession();
            return View();
        }
        private void KillOrderSession()
        {
            Session["UserPaymentId"] = null;
            Session["UserPaymentMethod"] = null;
            Session["UserPaymentType"] = null;

        }
        public ActionResult NoAccess()
        {
            Response.StatusCode = 301;
            return View();

        }

    }
}