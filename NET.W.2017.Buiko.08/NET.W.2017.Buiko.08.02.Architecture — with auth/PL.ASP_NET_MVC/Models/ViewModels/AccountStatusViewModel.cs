using System.ComponentModel.DataAnnotations;

namespace PL.ASP_NET_MVC.Models.ViewModels
{
    public class AccountStatusViewModel
    {
        [Display(Name = "Account type")]
        public string Type { get; set; }

        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Owner first name")]
        public string OwnerFirstName { get; set; }

        [Display(Name = "Owner second name")]
        public string OwnerSecondName { get; set; }

        [Display(Name = "Current sum")]
        [DataType(DataType.Currency)]
        public string Sum { get; set; }

        [Display(Name = "Bonus points")]
        public string BonusPoints { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }
    }
}