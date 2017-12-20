using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class AccountOperationViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Remote("CheckAccountNumber", "Account", ErrorMessage = "Account with this id does not exist")]
        public string AccountNumber { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "500000", ErrorMessage = "The initial sum must be at least 0 and not more than 500,000")]
        public decimal Sum { get; set; }
    }
}