using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.ASP_NET_MVC.Models.ViewModels.AuthModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Passwod { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Compare(nameof(Passwod), ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswodCinfirm { get; set; }
    }
}