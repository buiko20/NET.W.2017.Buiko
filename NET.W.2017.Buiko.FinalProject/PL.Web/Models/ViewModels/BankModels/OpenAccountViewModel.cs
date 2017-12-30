using System.ComponentModel.DataAnnotations;
using BLL.Interface.Entities;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class OpenAccountViewModel
    {
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }

        [Display(Name = "Initial sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "The initial sum must be at least 0 and not more than 50,000")]
        public decimal Sum { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}