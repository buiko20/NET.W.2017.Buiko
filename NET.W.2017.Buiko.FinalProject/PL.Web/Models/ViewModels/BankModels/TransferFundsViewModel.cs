using System.ComponentModel.DataAnnotations;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class TransferFundsViewModel
    {
        [Display(Name = "From account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string FromAccountNumber { get; set; }

        [Display(Name = "Recipient's email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string ToEmail { get; set; }

        [Display(Name = "Recipient's account number")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public string ToAccountNumber { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The sum must be at least 0 and not more than 50,000,0")]
        public decimal Sum { get; set; }
    }
}