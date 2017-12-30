using System;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Services.Accounts;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    internal static class BllEntityMapper
    {
        public static DalBankUser ToDalBankUser(this BankUser bankUser, string passwordHash) =>
            new DalBankUser
            {
                FirstName = bankUser.FirstName,
                SecondName = bankUser.SecondName,
                Email = bankUser.Email,
                PasswordHash = passwordHash,
                Role = bankUser.Role
            };

        public static BankUser ToInterfaceBankUser(this DalBankUser dalBankUser)
        {
            var result = new BankUser
            {
                Email = dalBankUser.Email,
                FirstName = dalBankUser.FirstName,
                SecondName = dalBankUser.SecondName,
                Role = dalBankUser.Role
            };

            result.Accounts.AddRange(dalBankUser.Accounts.Select(account => account.ToInterfaceAccount(result)));
            return result;
        }

        public static Account ToInterfaceAccount(this DalAccount dalAccount, BankUser bankUser) =>
            new Account
            {
                Id = CryptographyHelper.Decrypt(dalAccount.Id, dalAccount.BankUser.Email),
                BankUser = bankUser,
                Sum = dalAccount.Sum,
                Type = GetAccountType(dalAccount.Type),
                BonusPoints = dalAccount.BonusPoints
            };

        public static DalAccount ToDalAccount(this BllAccount bllAccount) =>
            new DalAccount
            {
                Id = bllAccount.Id,
                BonusPoints = bllAccount.BonusPoints,
                Sum = bllAccount.Sum,
                Type = GetAccountType(bllAccount.GetType()),
                BankUser = bllAccount.BankUser.ToDalBankUser(string.Empty) 
            };

        public static BllAccount ToBllAccount(this DalAccount dalAccount)
        {
            switch (dalAccount.Type)
            {
                case "Platinum":
                    return new BllPlatinumAccount(dalAccount.Id, dalAccount.Sum, dalAccount.BonusPoints, dalAccount.BankUser.ToInterfaceBankUser());
                case "Gold":
                    return new BllGoldAccount(dalAccount.Id, dalAccount.Sum, dalAccount.BonusPoints, dalAccount.BankUser.ToInterfaceBankUser());
                case "Base": 
                default:
                    return new BllBaseAccount(dalAccount.Id, dalAccount.Sum, dalAccount.BonusPoints, dalAccount.BankUser.ToInterfaceBankUser());
            }
        }

        public static Account ToInterfaceAccount(this BllAccount bllAccount, BankUser bankUser) =>
            new Account
            {
                Id = CryptographyHelper.Decrypt(bllAccount.Id, bllAccount.BankUser.Email),
                BankUser = bankUser,
                Sum = bllAccount.Sum,
                Type = GetAccountType(bllAccount.GetType().Name),
                BonusPoints = bllAccount.BonusPoints
            };

        private static AccountType GetAccountType(string typeName)
        {
            if (typeName.Contains("Gold"))
            {
                return AccountType.Gold;
            }

            if (typeName.Contains("Platinum"))
            {
                return AccountType.Platinum;
            }

            return AccountType.Base;
        }

        private static string GetAccountType(Type type)
        {
            string typeName = type.Name;
            if (typeName.Contains("Gold"))
            {
                return "Gold";
            }

            if (typeName.Contains("Platinum"))
            {
                return "Platinum";
            }

            return "Base";
        }
    }
}
