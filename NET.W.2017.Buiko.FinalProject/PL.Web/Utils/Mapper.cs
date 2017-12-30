using BLL.Interface.Entities;
using PL.Web.Models.ViewModels.BankModels;

namespace PL.Web.Utils
{
    public static class Mapper
    {
        public static AccountInfoViewModel ToInfoViewModel(this Account bllAccount) =>
            new AccountInfoViewModel
            {
                AccountNumber = bllAccount.Id,
                Sum = bllAccount.Sum,
                BonusPoints = bllAccount.BonusPoints,
                AccountType = bllAccount.Type
            };

        public static BankUserViewModel ToBankUserViewModel(this BankUser bankUser) =>
            new BankUserViewModel
            {
                Email = bankUser.Email,
                FirstName = bankUser.FirstName,
                SecondName = bankUser.SecondName,
                Role = bankUser.Role,
                AccountNumber = bankUser.Accounts.Count
            };
    }
}