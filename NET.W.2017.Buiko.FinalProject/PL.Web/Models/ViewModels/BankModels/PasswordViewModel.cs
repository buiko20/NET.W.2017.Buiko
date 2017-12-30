using System.ComponentModel.DataAnnotations;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class PasswordViewModel
    {
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}