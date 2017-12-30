using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class TransferFundsViewModel
    {
        [Display(Name = "From account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string FromAccountNumber { get; set; }

        [Display(Name = "To account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string ToAccountNumber { get; set; }

        [Display(Name = "Transfer sum")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 500,000")]
        public decimal Sum { get; set; }
    }
}