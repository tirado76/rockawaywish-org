using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using InteractiveMembership.Core.Constants;
using InteractiveMembership.Core.Models;

using RockawayWish.Data.Providers;

namespace RockawayWish.Data.Tests
{
    [TestClass]
    public class AccountsProviderTests
    {

        #region Constructor
        public AccountsProviderTests()
        {
            _Provider = new AccountsProvider();
            _UserModel = new UserModel();
        }
        #endregion

        #region Private Properties
        private AccountsProvider _Provider;
        private UserModel _UserModel;
        private IEnumerable<UserModel> _UserModelList;

        Guid _ApplicationId = new Guid(ConfigurationManager.AppSettings["Test-Application-Id"]);
        Guid _UserId = new Guid(ConfigurationManager.AppSettings["Test-User-Id"]);
        Guid _UserTokenId = new Guid(ConfigurationManager.AppSettings["Test-User-Token-Id"]);
        Guid _ActivationAccessToken = new Guid(ConfigurationManager.AppSettings["Test-User-Activation-Access-Token"]);
        Guid _ResetPassowrdAccessToken = new Guid(ConfigurationManager.AppSettings["Test-User-Reset-Password-Access-Token"]);
        Guid _EmptyGuid = new Guid(ConfigurationManager.AppSettings["Test-Empty-Guid"]);
        string _FirstName = ConfigurationManager.AppSettings["Test-User-First-Name"].ToString();
        string _LastName = ConfigurationManager.AppSettings["Test-User-Last-Name"].ToString();
        string _EmailAddress = ConfigurationManager.AppSettings["Test-User-Email-Address"].ToString();
        string _Password = ConfigurationManager.AppSettings["Test-User-Password"].ToString();
        string _PasswordNew = ConfigurationManager.AppSettings["Test-User-Password-New"].ToString();
        string _Address = ConfigurationManager.AppSettings["Test-User-Address"].ToString();
        string _City = ConfigurationManager.AppSettings["Test-User-City"].ToString();
        string _Zip = ConfigurationManager.AppSettings["Test-User-Zip"].ToString();
        string _State = ConfigurationManager.AppSettings["Test-User-State"].ToString();
        string _Country = ConfigurationManager.AppSettings["Test-User-Country"].ToString();
        string _CellPhone = ConfigurationManager.AppSettings["Test-User-Cell-Phone"].ToString();
        string _HomePhone = ConfigurationManager.AppSettings["Test-User-Home-Phone"].ToString();
        int _YearJoined = int.Parse(ConfigurationManager.AppSettings["Test-User-Year-Joined"].ToString());
        bool _IsUser = Convert.ToBoolean(ConfigurationManager.AppSettings["Test-User-Is-User"].ToString());
        bool _isActive = Convert.ToBoolean(ConfigurationManager.AppSettings["Test-User-Is-Active"].ToString());
        bool _IsAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["Test-User-Is-Admin"].ToString());
        bool _IsSuperAdmin = Convert.ToBoolean(ConfigurationManager.AppSettings["Test-User-Is-Super-Admin"].ToString());
        bool _IsDonator = Convert.ToBoolean(ConfigurationManager.AppSettings["Test-User-Is-Donator"].ToString());

        #endregion

        [TestMethod]
        public void FullTest()
        {
            // create user & test
            _UserModel = _Provider.Create(UnitTestConfig.ApplicationId, UnitTestConfig.UserEmailAddress, UnitTestConfig.UserPassword, UnitTestConfig.UserFirstName, UnitTestConfig.UserLastName, UnitTestConfig.UserIsActive, UnitTestConfig.UserIsUser, UnitTestConfig.UserIsDonator, UnitTestConfig.UserIsAdmin, UnitTestConfig.UserIsSuperAdmin, UnitTestConfig.UserYearJoined, UnitTestConfig.UserAddress, UnitTestConfig.UserCity, UnitTestConfig.UserState, UnitTestConfig.UserCountry, UnitTestConfig.UserZip, UnitTestConfig.UserPhone, UnitTestConfig.UserCellPhone).Result;
            Assert.IsTrue(_UserModel.Status != 1);
            Assert.IsTrue(_UserModel.UserId != null);

            // set test userId variable from return userId
            //UnitTestConfig.UserId = _UserModel.UserId;

            // update user & test
            _UserModel = _Provider.Update(UnitTestConfig.ApplicationId, UnitTestConfig.UserId, UnitTestConfig.UserIsActiveUpdated, UnitTestConfig.UserIsUserUpdated, UnitTestConfig.UserIsDonatorUpdated, UnitTestConfig.UserIsAdminUpdated, UnitTestConfig.UserIsSuperAdminUpdated, UnitTestConfig.UserYearJoinedUpdated, UnitTestConfig.UserEmailAddressUpdated, UnitTestConfig.UserPasswordUpdated, UnitTestConfig.UserFirstNameUpdated, UnitTestConfig.UserLastNameUpdated, UnitTestConfig.UserAddressUpdated, UnitTestConfig.UserCityUpdated, UnitTestConfig.UserStateUpdated, UnitTestConfig.UserCountryUpdated, UnitTestConfig.UserZipUpdated, UnitTestConfig.UserPhoneUpdated, UnitTestConfig.UserCellPhoneUpdated).Result;
            Assert.IsTrue(_UserModel.Status != 1);
            Assert.IsTrue(_UserModel.UserId != null);

            // get users & test
            _UserModelList = (List<UserModel>)_Provider.Get(UnitTestConfig.ApplicationId).Result;
            Assert.IsTrue(_UserModel.Status != 1);

            // get users & test
            _UserModelList = (List<UserModel>)_Provider.Get(UnitTestConfig.ApplicationId).Result;
            Assert.IsTrue(_UserModel.Status != 1);

        }

        /// <summary>
        /// Creates a user
        /// </summary>
        [TestMethod]
        public void Create()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.Create(_ApplicationId, _EmailAddress, _Password, _FirstName, _LastName, _isActive, _IsUser, _IsDonator, _IsAdmin, _IsSuperAdmin, _YearJoined, _Address, _City, _State, _City, _Zip, _HomePhone, _CellPhone).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        [TestMethod]
        public void Get()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModelList = _Provider.Get(UnitTestConfig.ApplicationId).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModelList != null);
            #endregion
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        [TestMethod]
        public void GetById()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.GetById(UnitTestConfig.ApplicationId, UnitTestConfig.UserId).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Gets a user by email
        /// </summary>
        [TestMethod]
        public void GetByEmail()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.GetByEmail(UnitTestConfig.ApplicationId, UnitTestConfig.UserEmailAddress).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        [TestMethod]
        public void Update()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.Update(UnitTestConfig.ApplicationId, UnitTestConfig.UserId, UnitTestConfig.UserIsActiveUpdated, UnitTestConfig.UserIsUserUpdated, UnitTestConfig.UserIsDonatorUpdated, UnitTestConfig.UserIsAdminUpdated, UnitTestConfig.UserIsSuperAdminUpdated, UnitTestConfig.UserYearJoinedUpdated, UnitTestConfig.UserEmailAddressUpdated, UnitTestConfig.UserPasswordUpdated, UnitTestConfig.UserFirstNameUpdated, UnitTestConfig.UserLastNameUpdated, UnitTestConfig.UserAddressUpdated, UnitTestConfig.UserCityUpdated, UnitTestConfig.UserStateUpdated, UnitTestConfig.UserCountryUpdated, UnitTestConfig.UserZipUpdated, UnitTestConfig.UserPhoneUpdated, UnitTestConfig.UserCellPhoneUpdated).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        [TestMethod]
        public void Delete()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.Delete(UnitTestConfig.ApplicationId, UnitTestConfig.UserId).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// creates a user token
        /// </summary>
        [TestMethod]
        public void CreateToken()
        {

            var result = _Provider.CreateToken(UnitTestConfig.ApplicationId, UnitTestConfig.UserId, UnitTestConfig.UserTokenId, UnitTestConfig.UserTokenTypeId);

            // Get the actual result from the task
            var viewresult = result.Result;

            // convert result data to list
            _UserModel = (UserModel)viewresult;


            ////Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(_UserModel);
            Assert.IsTrue(_UserModel.Status != 1);
            Assert.IsTrue(_UserModel.Status == 0);

            Assert.IsTrue(_UserModel.AccessToken != null);
        }

        /// <summary>
        /// Deletes a user token
        /// </summary>
        [TestMethod]
        public void RequestToken()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.RequestToken(UnitTestConfig.ApplicationId, UnitTestConfig.UserEmailAddressUpdated, UnitTestConfig.UserPasswordUpdated).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Validates and deletes a user token
        /// </summary>
        [TestMethod]
        public void ValidateToken()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.ValidateToken(UnitTestConfig.ApplicationId, UnitTestConfig.UserId, UnitTestConfig.UserTokenId, UnitTestConfig.UserTokenTypeId).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Deletes a user token
        /// </summary>
        [TestMethod]
        public void DeleteToken()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.DeleteToken(UnitTestConfig.ApplicationId, UnitTestConfig.UserId, UnitTestConfig.UserTokenTypeId).Result;
            #endregion

            #region Assert
            // test status
            Assert.IsTrue(_UserModel != null && _UserModel.Status == 0);
            #endregion
        }

        /// <summary>
        /// Activates a user
        /// </summary>
        [TestMethod]
        public void Activate()
        {
            #region Arrange

            #endregion

            #region Act
            _UserModel = _Provider.Activate(_ApplicationId, _UserId, _ActivationAccessToken).Result;
            #endregion

            #region Assert
            Assert.IsTrue(_UserModel != null && _UserModel.Status != 1);
            #endregion
        }

        /// <summary>
        /// Validates a user's login credentials
        /// </summary>
        [TestMethod]
        public void ValidateLogin()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.ValidateLogin(_ApplicationId, _EmailAddress, _Password).Result;
            #endregion

            #region Assert
            // test status is successful
            Assert.IsTrue(_UserModel != null && _UserModel.Status != 1);

            // test access token returned 
            Assert.IsTrue(_UserModel.AccessToken != null && _UserModel.AccessToken != _EmptyGuid);
            #endregion
        }

        /// <summary>
        /// User forgot their password
        /// </summary>
        [TestMethod]
        public void ForgotPassword()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.ForgotPassword(_ApplicationId, _EmailAddress).Result;
            #endregion

            #region Assert
            // test status is successful
            Assert.IsTrue(_UserModel != null && _UserModel.Status != 1);
            #endregion
        }

        /// Resets a user password
        /// </summary>
        [TestMethod]
        public void ResetPassword()
        {
            #region Arrange
            #endregion

            #region Act
            _UserModel = _Provider.ResetPassword(_ApplicationId, _UserId, _PasswordNew, _ResetPassowrdAccessToken).Result;
            #endregion

            #region Assert
            Assert.IsTrue(_UserModel != null && _UserModel.Status != 1);
            #endregion
        }

    }
}
