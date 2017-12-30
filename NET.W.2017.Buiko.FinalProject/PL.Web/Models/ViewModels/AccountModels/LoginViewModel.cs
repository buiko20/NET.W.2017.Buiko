using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.Web.Models.ViewModels.AccountModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
      //  [Remote("VerifyUserExists", "Account", ErrorMessage = "User not found")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}