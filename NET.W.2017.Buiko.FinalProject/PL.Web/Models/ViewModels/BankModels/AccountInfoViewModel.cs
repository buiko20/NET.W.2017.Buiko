using System.ComponentModel.DataAnnotations;
using BLL.Interface.Entities;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class AccountInfoViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }

        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType AccountType { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 50,000,0")]
        public decimal Sum { get; set; }

        [Display(Name = "Bonus points")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public int BonusPoints { get; set; }
    }
}