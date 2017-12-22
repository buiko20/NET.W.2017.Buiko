using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class AccountNumberViewModel
    {
        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string AccountNumber { get; set; }
    }
}