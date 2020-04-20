using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using InteractiveMembership.Core.Models;

namespace RockawayWish.Web.Models
{
    public class UserPaymentsViewModel
    {
        public UserPaymentsViewModel()
        {
            UserPaymentsModel = new UserPaymentsModel();
            DuesPaymentList = new Dictionary<string, List<UserPaymentsModel>>();

        }
        #region Public Properties
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public UserModel UserModel { get; set; }
        public Dictionary<string, List<UserPaymentsModel>> DuesPaymentList { get; set; }
        public UserPaymentsModel UserPaymentsModel { get; set; }
        #endregion
    }
}