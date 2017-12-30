using System.ComponentModel.DataAnnotations;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class DepositOrWithdrawViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The sum must be at least 0 and not more than 50,000,0")]
        public decimal Sum { get; set; }
    }
}