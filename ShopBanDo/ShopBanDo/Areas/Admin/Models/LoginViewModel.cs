using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShopBanDo.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = ("Email cannot be empty"))]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Wrong email type")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}
