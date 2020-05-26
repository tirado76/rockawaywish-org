using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Name { get; set; }
        [Display(Name = "Name:")]
        [Required(ErrorMessage = "Name is required")]
        [EmailAddress(ErrorMessage = "Invalid name")]
        public string Email { get; set; }

        [Display(Name = "Message:")]
        [Required(ErrorMessage = "Message is required")]
        public string ContactMessage { get; set; }
        
        #endregion
    }
}