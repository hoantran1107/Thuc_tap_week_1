using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.ModelView
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Current password")]
        [Required(ErrorMessage = "Password cannot be empty")]
        public string PasswordNow { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(5, ErrorMessage = "Passwords must contain at least five characters")]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage = "Confirm passwords must contain at least five characters")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password and new password dont match")]
        public string ConfirmPassword { get; set; }
    }
}
