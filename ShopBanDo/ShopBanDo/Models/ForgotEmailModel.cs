using System.ComponentModel.DataAnnotations;

namespace ShopBanDo.Models
{
    public class ForgotEmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
