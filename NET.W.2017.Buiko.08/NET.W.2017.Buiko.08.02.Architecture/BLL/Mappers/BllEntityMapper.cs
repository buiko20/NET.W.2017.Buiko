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
                AccountType = account.GetType(),
                BonusPoints = account.BonusPoints,
                CurrentSum = account.CurrentSum,
                Id = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerSecondName = account.OwnerSecondName
            };

        internal static Account ToBllAccount(this DalAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.Id,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerSecondName,
                dalAccount.CurrentSum,
                dalAccount.BonusPoints);
    }
}
