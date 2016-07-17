using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;

using RockawayWish.Web.Models;

using InteractiveMembership.Core.Constants;
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
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                var validateToken = await _userProvider.ValidateToken(new Guid(Config.ApplicationId), requestToken.UserId, requestToken.AccessToken);
                if (validateToken.Status == 0)
                {
                    if (!validateToken.IsUser && !validateToken.IsAdmin && !validateToken.IsSuperAdmin)
                    {
                        ModelState.AddModelError("", "Access Denied");
                        return View(model);
                    }

                    string ticketName = string.Format("{0}|{1}|{2}", string.Format("{0} {1}", validateToken.FirstName, validateToken.LastName), validateToken.UserId.ToString(), requestToken.AccessToken);
                    //string ticketName = string.Format("{0}|{1}|{2}|{3}", model.FirstName, model.LastName, result.UserId.ToString(), model.Email);
                    SetAuthenticatation(ticketName, true);

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
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // register user
                var result = await _userProvider.Create(new Guid(Config.ApplicationId), model.Email, model.Password, model.FirstName, model.LastName, false, false, false, false, false);

                if (result.Status == 0)
                {
                    // redirect user to home page
                    return Redirect("~/account/RegisterConfirmation");

                }
                else
                {
                    ModelState.AddModelError("", result.Message);

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterConfirmation()
        {
            return View();
        }

        public async Task<ActionResult> SignOut()
        {
            if (Request.IsAuthenticated)
            {
                    // request access token
                var requestToken = await _userProvider.DeleteToken(new Guid(Config.ApplicationId), this.UserId);
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



        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Redirect("~/account/ForgotPasswordConfirmation");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Redirect("~/account/ResetPasswordConfirmation");
            }
            return View();
        }

        [AllowAnonymous]
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