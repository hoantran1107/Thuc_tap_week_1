using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShopBanDo.ModelView
{
    public class ResetPasswordModelView
    {
        [Key]
        public int CustomerId { get; set; }

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
