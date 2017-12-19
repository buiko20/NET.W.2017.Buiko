using System.ComponentModel.DataAnnotations;
using BLL.Interface.AccountService;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class OpenAccountViewModel
    {
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountType Type { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerFirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerSecondName { get; set; }

        [Display(Name = "Initial sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0", "50000", ErrorMessage = "The initial sum must be at least 0 and not more than 50,000")]
        public decimal Sum { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }
    }
}