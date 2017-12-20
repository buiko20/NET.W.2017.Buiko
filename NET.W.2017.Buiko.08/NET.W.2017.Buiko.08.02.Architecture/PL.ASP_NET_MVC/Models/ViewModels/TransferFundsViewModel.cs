using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class TransferFundsViewModel
    {
        [Display(Name = "Source account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Remote("CheckAccountNumber", "Account", ErrorMessage = "Account with this id does not exist")]
        public string FromAccountNumber { get; set; }

        [Display(Name = "Destination account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Remote("CheckAccountNumber", "Account", ErrorMessage = "Account with this id does not exist")]
        public string ToAccountNumber { get; set; }

        [Display(Name = "Transfer sum")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 500,000")]
        public decimal Sum { get; set; }
    }
}