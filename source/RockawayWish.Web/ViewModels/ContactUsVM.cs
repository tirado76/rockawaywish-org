using InteractiveMembership.Core.ViewModels;

namespace RockawayWish.Web.ViewModels
{
    public class ContactUsVM : BaseVM
    {
        #region Constructor
        public ContactUsVM()
        {
        }
        #endregion
        #region Public Properties
        public string Name { get; set; }
        public string Email { get; set; }
        #endregion
    }
}