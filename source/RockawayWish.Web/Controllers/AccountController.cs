using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Constants.EndPointSettings;
using InteractiveMembership.Core.Interfaces;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.ViewModels;

using RockawayWish.Web.Repositories;

namespace RockawayWish.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor
        public AccountController(Guid apiKey, Guid applicationId, AppSettingsConfig appSettings)
        {
            _Repository = new AccountRepository(apiKey, applicationId, appSettings);
        }
        #endregion

        #region Private Properties
        IAccountsWebRepository _Repository;
        #endregion

        #region Public Methods

        /// <summary>
        /// User registration form
        /// </summary>
        /// <returns>RegisterVM</returns>
        [Route(SiteEndPointsConfig.Register)]
        public async Task<ActionResult> Register()
        {
            // return
            return View(await _Repository.Register());
        }

        /// <summary>
        /// User submits registration form
        /// Creates user activation access token
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>RegisterVM</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(SiteEndPointsConfig.Register)]
        public async Task<ActionResult> Register(RegisterVM vm)
        {
            // return
            return View(await _Repository.Register(vm));
        }

        /// <summary>
        /// User completes registration
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>RegisterCompleteVM</returns>
        [Route(SiteEndPointsConfig.RegisterComplete)]
        public async Task<ActionResult> RegisterComplete()
        {
            // return
            return View(await _Repository.RegisterComplete());
        }

        /// <summary>
        /// Activates a user account, sets user account to active
        /// Validates user activation access token and deletes it
        /// pD = applicationId
        /// sD = userId
        /// tP = tokenId
        /// </summary>
        /// <param name="pD"></param>
        /// <param name="sD"></param>
        /// <param name="tP"></param>
        /// <returns>ActivateVM</returns>
        [Route(SiteEndPointsConfig.Activate)]
        public async Task<ActionResult> Activate(Guid pD, Guid sD, Guid tP)
        {
            // return
            return View(await _Repository.Activate(pD, sD, tP));
        }

        /// <summary>
        /// User forgot password form
        /// </summary>
        /// <returns>ForgotPasswordVM</returns>
        [Route(SiteEndPointsConfig.ForgotPassword)]
        public async Task<ActionResult> ForgotPassword()
        {
            // return
            return View(await _Repository.ForgotPassword());
        }

        /// <summary>
        /// User submits forgot password form
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>ForgotPasswordVM</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(SiteEndPointsConfig.ForgotPassword)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            // execute user forgot password
            var forgotPassword = await _Repository.ForgotPassword(vm);

            // check if successfully
            if (forgotPassword != null && forgotPassword.Status == 0)
            {
                // a user reset password access token was created
                // redirect to complete page
                return RedirectPermanent(string.Format("~/{0}", SiteEndPointsConfig.ForgotPasswordComplete));
            }
            else
            {
                vm.Status = forgotPassword.Status;
                vm.Message = forgotPassword.Message;
            }

            // return
            return View(vm);
        }

        /// <summary>
        /// User completes forgot password submission
        /// </summary>
        /// <returns>ForgotPasswordCompleteVM</returns>
        [Route(SiteEndPointsConfig.ForgotPasswordComplete)]
        public async Task<ActionResult> ForgotPasswordComplete()
        {
            // return
            return View(await _Repository.ForgotPasswordComplete());
        }

        /// <summary>
        /// Checks if user site access token exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserModel</returns>
        [Route(SiteEndPointsConfig.IsAuthenticated)]
        public async Task<UserModel> IsAuthenticated(Guid userId)
        {
            // return
            return await _Repository.IsAuthenticated(userId);
        }

        /// <summary>
        /// Users sign in form
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Route(SiteEndPointsConfig.Login)]
        public async Task<ActionResult> Login(string returnUrl)
        {
            // return
            return View(await _Repository.Login(returnUrl));
        }

        /// <summary>
        /// 1. User submits sign in form
        /// 2. Validate email and password
        /// 3. Creates user site access token if successful
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(SiteEndPointsConfig.Login)]
        public async Task<ActionResult> Login(LoginVM vm, string returnUrl)
        {
            // validate login ancd create user site access token
            var result = await _Repository.Login(vm, returnUrl);

            // check if successfully
            if (result != null && result.Status == 0)
            {
                // a user reset password access token was created
                // redirect to complete page
                if (!string.IsNullOrEmpty(returnUrl))
                    return RedirectPermanent(string.Format("~/{0}", returnUrl));
                else
                    return RedirectPermanent("~/");
            }
            else
            {
                vm.Status = result.Status;
                vm.Message = result.Message;

                // return
                return View(vm);
            }
        }

        /// <summary>
        /// 1. Check if user reset password access token exists
        /// 2. Show reset password form
        /// 3. Delete user reset password access token
        /// tD = applicationId
        /// sD = userId
        /// gD = tokenId
        /// </summary>
        /// <param name="tD"></param>
        /// <param name="sD"></param>
        /// <param name="gD"></param>
        /// <returns>ResetPasswordVM</returns>
        [Route(SiteEndPointsConfig.ResetPassword)]
        public async Task<ActionResult> ResetPassword(Guid tD, Guid sD, Guid gD)
        {
            // return
            return View(await _Repository.ResetPassword(tD, sD, gD));
        }

        /// <summary>
        /// 1. Submits user reset password form
        /// 2. Updates user password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>ResetPasswordVM</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(SiteEndPointsConfig.ResetPassword)]
        public async Task<ActionResult> ResetPassword(ResetPasswordVM vm)
        {
            // return
            return View(await _Repository.ResetPassword(vm));
        }

        /// <summary>
        /// Reset password complete page
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>ResetPasswordCompleteVM</returns>
        [Route(SiteEndPointsConfig.ResetPasswordComplete)]
        public async Task<ActionResult> ResetPasswordComplete(string msg)
        {
            // return
            return View(await _Repository.ResetPasswordComplete(msg));
        }

        /// <summary>
        /// 1. User signs out
        /// 2. Delete user site acces token
        /// </summary>
        /// <returns>SignOutCompleteVM</returns>
        [Route(SiteEndPointsConfig.SignOut)]
        public async Task<ActionResult> SignOut(Guid userId)
        {
            // return
            return View(await _Repository.SignOut(userId));
        }

        /// <summary>
        /// Sign out completion page
        /// </summary>
        /// <returns>SignOutCompleteVM</returns>
        [Route(SiteEndPointsConfig.SignOutComplete)]
        public async Task<ActionResult> SignOutComplete()
        {
            // return
            return View(await _Repository.SignOutComplete());
        }
        #endregion
    }
}