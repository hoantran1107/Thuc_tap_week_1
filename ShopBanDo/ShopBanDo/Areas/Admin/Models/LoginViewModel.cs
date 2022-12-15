using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShopBanDo.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = ("Vui lòng nhập Email"))]
        [Display(Name = "Địa chỉ Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(0, ErrorMessage = "Mật khẩu tối thiểu 5 ký tự")]
        public string Password { get; set; }
    }
}
