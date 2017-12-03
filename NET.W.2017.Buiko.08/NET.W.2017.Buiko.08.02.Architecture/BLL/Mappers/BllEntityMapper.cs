using System;
using BLL.Interface.Account;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    internal static class BllEntityMapper
    {
        internal static DalAccount ToDalAccount(this Account account) =>
            new DalAccount
            {
                AccountType = account.GetType().Name,
                BonusPoints = account.BonusPoints,
                CurrentSum = account.CurrentSum,
                Id = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName,
                OwnerEmail = account.OwnerEmail
            };

        internal static Account ToBllAccount(this DalAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                GetBllAccountType(dalAccount.AccountType),
                dalAccount.Id,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints,
                dalAccount.OwnerEmail);

        private static Type GetBllAccountType(string type)
        {
            if (type.Contains("Gold"))
            {
                return typeof(GoldAccount);
            }

            if (type.Contains("Platinum"))
            {
                return typeof(PlatinumAccount);
            }

            return typeof(BaseAccount);
        }
    }
}
