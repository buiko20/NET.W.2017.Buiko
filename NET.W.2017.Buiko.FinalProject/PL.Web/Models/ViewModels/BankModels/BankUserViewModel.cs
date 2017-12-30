using System.ComponentModel.DataAnnotations;

namespace PL.Web.Models.ViewModels.BankModels
{
    public class BankUserViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string SecondName { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        public string Role { get; set; }

        [Display(Name = "Account number")]
        [Required(ErrorMessage = "Field must be not empty", AllowEmptyStrings = false)]
        [Range(typeof(int), "0", "25", ErrorMessage = "Number must be not less than 0")]
        public int AccountNumber { get; set; }
    }
}