using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using InteractiveMembership.Core.Interfaces;
using InteractiveMembership.Core.Models;

using RockawayWish.Data.Repositories;

namespace RockawayWish.Data.Providers
{
    public class AccountsProvider : IUser
    {
        #region Constructor
        public AccountsProvider()
        {
            _Repository = new AccountsRepository();
        }
        #endregion

        #region Private Properties
        private IUser _Repository;
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="isActive"></param>
        /// <param name="isUser"></param>
        /// <param name="isDonator"></param>
        /// <param name="isAdmin"></param>
        /// <param name="isSuperAdmin"></param>
        /// <param name="yearJoined"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <param name="cellPhone"></param>
        /// <returns></returns>
        public async Task<UserModel> Create(Guid applicationId, string email, string password, string firstName, string lastName, bool isActive, bool isUser, bool isDonator, bool isAdmin, bool isSuperAdmin, int yearJoined, string address, string city, string state, string country, string zip, string phone, string cellPhone)
        {
            return await _Repository.Create(applicationId, email, password, firstName, lastName, isActive, isUser, isDonator, isAdmin, isSuperAdmin, yearJoined, address, city, state, country, zip, phone, cellPhone);
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> Get(Guid applicationId)
        {
            return await _Repository.Get(applicationId);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserModel> GetById(Guid applicationId, Guid userId)
        {
            return await _Repository.GetById(applicationId, userId);
        }

        /// <summary>
        /// Gets a user by email
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserModel> GetByEmail(Guid applicationId, string email)
        {
            return await _Repository.GetByEmail(applicationId, email);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="isActive"></param>
        /// <param name="isUser"></param>
        /// <param name="isDonator"></param>
        /// <param name="isAdmin"></param>
        /// <param name="isSuperAdmin"></param>
        /// <param name="yearJoined"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zip"></param>
        /// <param name="phone"></param>
        /// <param name="cellPhone"></param>
        /// <returns></returns>
        public async Task<UserModel> Update(Guid applicationId, Guid userId, bool isActive, bool isUser, bool isDonator, bool isAdmin, bool isSuperAdmin, int yearJoined, string email = null, string password = null, string firstName = null, string lastName = null, string address = null, string city = null, string state = null, string country = null, string zip = null, string phone = null, string cellPhone = null)
        {
            return await _Repository.Update(applicationId, userId, isActive, isUser, isDonator, isAdmin, isSuperAdmin, yearJoined, email, password, firstName, lastName, address, city, state, country, zip, phone, cellPhone);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserModel> Delete(Guid applicationId, Guid userId)
        {
            return await _Repository.Delete(applicationId, userId);
        }

        /// <summary>
        /// Creates a user token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="tokenId"></param>
        /// <param name="tokenTypeId"></param>
        /// <returns></returns>
        public async Task<UserModel> CreateToken(Guid applicationId, Guid userId, Guid tokenId, int tokenTypeId)
        {
            return await _Repository.CreateToken(applicationId, userId, tokenId, tokenTypeId);
        }

        /// <summary>
        /// Requests a user token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> RequestToken(Guid applicationId, string email, string password)
        {
            return await _Repository.RequestToken(applicationId, email, password);
        }

        /// <summary>
        /// Validates and deletes a user token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="tokenId"></param>
        /// <param name="tokenTypeId"></param>
        /// <returns></returns>
        public async Task<UserModel> ValidateToken(Guid applicationId, Guid userId, Guid tokenId, int tokenTypeId)
        {
            return await _Repository.ValidateToken(applicationId, userId, tokenId, tokenTypeId);
        }

        /// <summary>
        /// Deletes a user token
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="tokenTypeId"></param>
        /// <returns></returns>
        public async Task<UserModel> DeleteToken(Guid applicationId, Guid userId, int tokenTypeId)
        {
            return await _Repository.DeleteToken(applicationId, userId, tokenTypeId);
        }

        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserModel> Activate(Guid applicationId, Guid userId, Guid token)
        {
            return await _Repository.Activate(applicationId, userId, token);
        }

        /// <summary>
        /// Validates a user's login credentials
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> ValidateLogin(Guid applicationId, string email, string password)
        {
            return await _Repository.ValidateLogin(applicationId, email, password);
        }

        /// <summary>
        /// User forgets their password
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserModel> ForgotPassword(Guid applicationId, string email)
        {
            return await _Repository.ForgotPassword(applicationId, email);
        }

        /// <summary>
        /// Resets a user's password
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserModel> ResetPassword(Guid applicationId, Guid userId, string password, Guid token)
        {
            return await _Repository.ResetPassword(applicationId, userId, password, token);
        }

        /// <summary>
        /// Signs a user out
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserModel> SignOut(Guid applicationId, Guid userId)
        {
            return await _Repository.SignOut(applicationId, userId);
        }

        #endregion
    }
}
