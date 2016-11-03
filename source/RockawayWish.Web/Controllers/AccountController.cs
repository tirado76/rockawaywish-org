using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;

using RockawayWish.Web.Models;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Enums;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Controllers
{
    public class AccountController : BaseController
    {
        private UsersProvider _userProvider = new UsersProvider();
        private UserModel _userModel = new UserModel();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Account/Login
        [Route("signin")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("signin")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var requestToken = await _userProvider.RequestToken(new Guid(Config.ApplicationId), model.Email, model.Password);
            if (requestToken.Status == 0)
            {
                // validate access token and retrieve user object
                var validateToken = await _userProvider.ValidateToken(new Guid(Config.ApplicationId), requestToken.UserId, requestToken.AccessToken, (int)TokenType.SiteAccess);
                if (validateToken.Status == 0)
                {
                    if ((!validateToken.IsUser || !validateToken.IsActive) && !validateToken.IsAdmin && !validateToken.IsSuperAdmin)
                    {
                        ModelState.AddModelError("", "Access Denied");
                        return View(model);
                    }

                    string ticketName = string.Format("{0}|{1}|{2}", string.Format("{0} {1}", validateToken.FirstName, validateToken.LastName), validateToken.UserId.ToString(), requestToken.AccessToken);
                    //string ticketName = string.Format("{0}|{1}|{2}|{3}", model.FirstName, model.LastName, result.UserId.ToString(), model.Email);
                    SetAuthenticatation(ticketName, false);
                    return Redirect("~/members");
                }
                else
                {
                    ModelState.AddModelError("", validateToken.Message);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", requestToken.Message);
            }
            return View(model);

        }
        // GET: /Account/Register
        [Route("register")]
        public ActionResult Register()
        {
            return View(new RegisterViewModel{
                YearJoined = DateTime.Now.Year
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.YearJoined = DateTime.Now.Year;
                // register user
                var result = await _userProvider.Create(new Guid(Config.ApplicationId), model.Email, Guid.NewGuid().ToString(), model.FirstName, model.LastName, false, false, false, false, false, model.YearJoined, model.Address, model.City, model.State, model.Country, model.Zip, model.Phone, model.CellPhone);

                if (result.Status == 0)
                {

                    // send email to membership administrator with registration information
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<p>The following user has registered on the WISH of Rockaway website and needs to be approved.</p>");
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
                    sb.AppendLine("<p><a href=\"" + this.MembershipAdminUrl  + "\">Click here</a> to go to the admin panel to approve the user.</p>");
                    sb.AppendLine("<p>&nbsp;</p>");
                    sb.AppendLine("<p>Wish of Rockaway Membership Administration</p>");
                    sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                    var emailResult = this.SendEmail(this.MembershipAdminEmail, this.MembershipAdminName, "A user has registered on the WISH of Rockaway website", sb.ToString());


                    // redirect user to home page
                    return RedirectPermanent("~/RegisterConfirmation");

                }
                else
                {
                    ModelState.AddModelError("", result.Message);

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Route("registerconfirmation")]
        public ActionResult RegisterConfirmation()
        {
            return View();
        }

        [Route("signout")]
        public async Task<ActionResult> SignOut()
        {
            if (Request.IsAuthenticated)
            {
                    // request access token
                var requestToken = await _userProvider.DeleteToken(new Guid(Config.ApplicationId), this.UserId, (int)TokenType.SiteAccess);
                if (requestToken.Status == 0)
                {
                }
                // sign user out of Purina API
                FormsAuthentication.SignOut();

            }
            // get pageof user
            string url = Request.UrlReferrer.AbsolutePath;

            if (!string.IsNullOrEmpty(url))
            {
                if (url.ToLower().Contains("/members"))
                    return Redirect("~/");

                url = string.Format("~{0}", url);
                return Redirect(url);
            }
            else
            {
                return Redirect("~/");
            }


        }



        [Route("forgotpassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
            }

            // get user 
            var user = new UsersProvider().Get(this.ApplicationId).Result.Where(x => x.Email.ToLower().Equals(model.Email.ToLower())).FirstOrDefault();

            if (user != null)
            {
                Guid token = Guid.NewGuid();
                // send email
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<p>It seems that you have forgotten your password. No problem!</p>");
                sb.AppendLine("<p>To reset your password, click the following link or copy and paste the link into your browser:</p>");
                sb.AppendLine("<p><a href=\"" + string.Format("{0}?tu={1}&ta={2}&tk={3}", this.ResetPasswordEndpoint, user.UserId, user.ApplicationId, token) + "\">" + string.Format("{0}?tu={1}&ta={2}", this.ResetPasswordEndpoint, user.UserId, user.ApplicationId) + "</a></p>");
                sb.AppendLine("<p>If you did not request to have your password reset you can safely ignore this email. Rest assured your customer account is safe.</p>");
                sb.AppendLine("<p>If you need further assistance, please contact us at <a href=\"mailto:" + this.SmtpFromAddress + "\">" + this.SmtpFromAddress + "</a> or by dropping a comment <a href=\"" + this.ContactUsUrl + "\">here</a>.</p>");
                sb.AppendLine("<p>&nbsp;</p>");
                sb.AppendLine("<p>Regards,</p>");
                sb.AppendLine("<p>Wish of Rockaway</p>");
                sb.AppendFormat("<img src=\"{0}://{1}/content/images/logo.png\">", Request.Url.Scheme, "rockawaywish.org");
                var result = this.SendEmail(model.Email, user.FullName, this.ResetPasswordSubject, sb.ToString());

                if (result.Status == 0)
                {
                    // create user token
                    var userToken = new UsersProvider().CreateToken(this.ApplicationId, user.UserId, token, (int)TokenType.ResetPasswordAccess);

                    return RedirectPermanent("~/account/ForgotPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email doesn't exist");
            }


            
            return View();
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [Route("resetpassword")]
        public ActionResult ResetPassword(string tu, string ta, string tk)
        {
            // tu = userId
            // ta = applicationId
            // tk = tokenId
            ResetPasswordViewModel vm = new ResetPasswordViewModel();

            // validate the token
            UserModel userModel = new UsersProvider().ValidateToken(new Guid(ta), new Guid(tu), new Guid(tk), (int)TokenType.ResetPasswordAccess).Result;

            vm.Status = userModel.Status;
            vm.Message = userModel.Message;
            vm.UserId = new Guid(tu);
            vm.ApplicationId = new Guid(ta);

            // delete token if token if validated
            var deleteToken = new UsersProvider().DeleteToken(new Guid(ta), new Guid(tu), (int)TokenType.ResetPasswordAccess).Result;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("resetpassword")]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // get userinfo
            var user = new UsersProvider().GetById(this.ApplicationId, model.UserId).Result;

            // update user
            if (user.Status == 0)
            {
                var result = new UsersProvider().Update(model.ApplicationId, model.UserId, user.IsActive, user.IsUser, user.IsDonator, user.IsAdmin, user.IsSuperAdmin, user.YearJoined, user.Email, model.Password, null, null, null, null, null, null, null, null, null).Result;
                if (result.Status == 0)
                {
                    // delete token
                    //var deleteToken = new UsersProvider().DeleteToken(this.ApplicationId, model.UserId, (int)TokenType.ResetPasswordAccess).Result;

                    //if (deleteToken.Status == 0)
                    return RedirectPermanent("~/account/ResetPasswordConfirmation");
                    //else
                    //    ModelState.AddModelError("", deleteToken.Message);
                }

                ModelState.AddModelError("", result.Message);
            }
            else
            {

                ModelState.AddModelError("", user.Message);
            }

            ResetPasswordViewModel vm = new ResetPasswordViewModel();
            // make sure is user is valid
            UserModel userModel = new UsersProvider().GetById(model.ApplicationId, model.UserId).Result;

            vm.Status = userModel.Status;
            vm.Message = userModel.Message;
            vm.UserId = userModel.UserId;
            vm.ApplicationId = model.ApplicationId;


            return View(vm);
            //return View(model);
        }

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Private Methods
        private void SetAuthenticatation(string authTicketName, bool blPersistant)
        {
            FormsAuthentication.SetAuthCookie(authTicketName, blPersistant);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}