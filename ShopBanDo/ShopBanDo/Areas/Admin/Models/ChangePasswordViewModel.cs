using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShopBanDo.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name ="Enter Your Email")]
        public string Email { get; set; }
        [Display(Name = "Your Current Password")]
        [Required(ErrorMessage = "Please Enter Your Current Password")]
        public string PasswordNow { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please Enter Your New Password")]
        [MinLength(5, ErrorMessage = "Your Password Need To Be At Least 5 Characters Long")]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage = "Your Password Need To Be At Least 5 Characters Long")]
        [Display(Name = "Please Re-Enter Your Password")]
        [Compare("Password", ErrorMessage = "Your Password Is Incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
