using System.ComponentModel.DataAnnotations;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class CloseAccountViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}