using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.ModelView
{
    public class MuaHangVM
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Enter your full name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter your address")]
        public string Address { get; set; }
        public int PaymentID { get; set; }
        public string Note { get; set; }
    }
}
