using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class AccountNumberViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Remote("CheckAccountNumber", "Account", ErrorMessage = "Account with this id does not exist")]
        public string AccountNumber { get; set; }
    }
}