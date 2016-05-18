using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Models
{
    public class UserModel
    {
        #region Public Properties
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        #endregion

    }
    public class UserTokenModel 
    {
        #region Public Properties
        public Guid UserTokenId { get; set; }
        public Guid UserId { get; set; }
        public Guid TokenId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        #endregion

    }
}
