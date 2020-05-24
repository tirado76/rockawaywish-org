using System;
using System.Threading.Tasks;
using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Interfaces;
using InteractiveMembership.Core.Models;
using InteractiveMembership.Core.ViewModels;
using InteractiveMembership.Data.Providers;

namespace RockawayWish.Web.Repositories
{
    internal class AccountRepository : BaseRepository, IAccountsRepository
    {
        #region Constructor
        public AccountRepository(Guid apiKey, Guid applicationId,  AppSettingsConfig appSettings)
        {
            _AccountsDataProvider = new AccountsDataProvider(apiKey, applicationId, appSettings);
            _UserModel = new UserModel();
        }
        #endregion

        #region Private Properties
        AccountsDataProvider _AccountsDataProvider;
        UserModel _UserModel;
        #endregion

        #region Public Methods

        /// <summary>
        /// User registration form
        /// </summary>
        /// <returns>RegisterVM</returns>
        public async Task<RegisterVM> Register()
        {
            // set awaitable
            await Task.Delay(0);

            // return
            return new RegisterVM();
        }

        /// <summary>
        /// User submits registration form
        /// Creates user activation access token
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>RegisterVM</returns>
        public async Task<RegisterVM> Register(RegisterVM vm)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// User completes registration
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>RegisterCompleteVM</returns>
        public async Task<RegisterCompleteVM> RegisterComplete()
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Activates a user account
        /// Validates user activation access token and deletes it
        /// </summary>
        /// <param name="pD"></param>
        /// <param name="sD"></param>
        /// <param name="tP"></param>
        /// <returns>ActivateVM</returns>
        public async Task<ActivateVM> Activate(Guid pD, Guid sD, Guid tP)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// User forgot password form
        /// </summary>
        /// <returns>ForgotPasswordVM</returns>
        public async Task<ForgotPasswordVM> ForgotPassword()
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// User submits forgot password form
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>ForgotPasswordVM</returns>
        public async Task<ForgotPasswordVM> ForgotPassword(ForgotPasswordVM vm)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// User completes forgot password submission
        /// </summary>
        /// <returns>ForgotPasswordCompleteVM</returns>
        public async Task<ForgotPasswordCompleteVM> ForgotPasswordComplete()
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if user site access token exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> IsAuthenticated(Guid userId)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Users sign in form
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<LoginVM> Login(string returnUrl)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// User submits sign in form
        /// Creates user site access token if successful
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<LoginVM> Login(LoginVM vm, string returnUrl)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 1. Check if user reset password access token exists
        /// 2. Show reset password form
        /// 3. Delete user reset password access token
        /// </summary>
        /// <param name="tD"></param>
        /// <param name="sD"></param>
        /// <param name="gD"></param>
        /// <returns>ResetPasswordVM</returns>
        public async Task<ResetPasswordVM> ResetPassword(Guid tD, Guid sD, Guid gD)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 1. Submits user reset password form
        /// 2. Updates user password
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>ResetPasswordVM</returns>
        public async Task<ResetPasswordVM> ResetPassword(ResetPasswordVM vm)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reset password complete page
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>ResetPasswordCompleteVM</returns>
        public async Task<ResetPasswordCompleteVM> ResetPasswordComplete(string msg)
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 1. User signs out
        /// 2. Delete user site acces token
        /// </summary>
        /// <returns>SignOutCompleteVM</returns>
        public async Task<SignOutCompleteVM> SignOut()
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sign out completion page
        /// </summary>
        /// <returns>SignOutCompleteVM</returns>
        public async Task<SignOutCompleteVM> SignOutComplete()
        {
            // set awaitable
            await Task.Delay(0);
            throw new NotImplementedException();
        }
        #endregion
    }
}