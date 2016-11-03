using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RockawayWish.Web.Models
{

    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }

}
