using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET_MVC.Models.ViewModels.AuthModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Passwod { get; set; }
    }
}