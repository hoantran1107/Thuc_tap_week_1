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

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Enter your full name again")]
        public string FullName { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "Enter your email again")]
        [DataType(DataType.EmailAddress)]
        //send ajax request,tra ve kieu json voi action va controller
        //kiem tra email da ton tai
        [Remote(action: "ValidateEmail", controller: "Accounts")]
        public string Email { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Enter your phone number")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        //kiem tra dien thoai da ton tai
        [Remote(action: "ValidatePhone", controller: "Accounts")]
        public string Phone { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter your password")]
        [MinLength(5, ErrorMessage = "Password's length must be at least 5 characters")]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage = "Password's length must be at least 5 characters")]
        [Display(Name = "Enter your password again")]
        //so sanh pass
        [Compare("Password", ErrorMessage = "Password is not correct")]
        public string ConfirmPassword { get; set; }
    }
}
