using System.Linq;
using DAL.Interface.DTO;
using ORM.Model;

namespace DAL.Mappers
{
    internal static class DalEntityMapper
    {
        public static BankUser ToOrmBankUser(this DalBankUser dalBankUser) =>
            new BankUser
            {
                Email = dalBankUser.Email,
                FirstName = dalBankUser.FirstName,
                SecondName = dalBankUser.SecondName,
                PasswordHash = dalBankUser.PasswordHash,
                Role = new Role { RoleInfo = dalBankUser.Role }
            };

        public static DalBankUser ToDalBankUser(this BankUser bankUser)
        {
            var result = new DalBankUser
            {
                Email = bankUser.Email,
                FirstName = bankUser.FirstName,
                SecondName = bankUser.SecondName,
                PasswordHash = bankUser.PasswordHash,
                Role = bankUser.Role.RoleInfo
            };

            result.Accounts.AddRange(bankUser.Accounts.Select(account => account.ToDalAccount(result)));
            return result;
        }
            
        public static Account ToOrmAccount(this DalAccount dalAccount, BankUser bankUser) =>
            new Account
            {
                AccountId = dalAccount.Id,
                AccountType = new AccountType { Type = dalAccount.Type },
                Sum = dalAccount.Sum,
                BonusPoints = dalAccount.BonusPoints,
                BankUser = bankUser,
                BankUserId = bankUser.Id
            };

        public static DalAccount ToDalAccount(this Account account, DalBankUser dalBankUser) =>
            new DalAccount
            {
                Id = account.AccountId,
                Sum = account.Sum,
                BonusPoints = account.BonusPoints,
                Type = account.AccountType.Type,
                BankUser = dalBankUser
            };
    }
}
