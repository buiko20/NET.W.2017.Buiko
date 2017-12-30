using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.Web.Models.ViewModels.AccountModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
      //  [Remote("VerifyUserNotExists", "Account", ErrorMessage = "User already exists")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string SecondName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }
}