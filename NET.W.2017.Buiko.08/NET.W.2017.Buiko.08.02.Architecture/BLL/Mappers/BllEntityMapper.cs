using System;
using BLL.Interface.Account;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static DalAccount ToDalAccount(this Account account) =>
            new DalAccount
            {
                AccountType = account.GetType(),
                BonusPoints = account.BonusPoints,
                CurrentSum = account.CurrentSum,
                Id = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName
            };

        public static Account ToBllAccount(this DalAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.Id,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints);
    }
}
