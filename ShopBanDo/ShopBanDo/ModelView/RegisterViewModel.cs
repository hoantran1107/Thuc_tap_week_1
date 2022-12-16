using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanDo.ModelView
{
    public class RegisterViewModel
    {
        [Key]
        public int CustomerId { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name cannot be empty.")]
        public string FullName { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "Email cannot be empty.")]
        [DataType(DataType.EmailAddress)]
        //send ajax request,tra ve kieu json voi action va controller
        //kiem tra email da ton tai
        [Remote(action: "ValidateEmail", controller: "Accounts")]
        public string Email { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Phone cannot be empty.")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        //kiem tra dien thoai da ton tai
        [Remote(action: "ValidatePhone", controller: "Accounts")]
        public string Phone { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be empty.")]
        [MinLength(5, ErrorMessage = "Passwords must contain at least five characters")]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage = "Passwords must contain at least five characters")]
        [Display(Name = "Repassword")]
        //so sanh pass
        [Compare("Password", ErrorMessage = "Repassword doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
