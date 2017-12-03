using BLL.Interface.AccountService;
using BLL.Services;
using DAL.BinaryFile;

namespace DependencyResolver
{
    public static class SimpleDependencyResolver
    {
        private const string DataFilePath = @"accounts.bin";

        public static IAccountService GetAccountService()
        {
            var accountRepository = new BinaryFileAccountRepository(DataFilePath);
            return new AccountService(accountRepository);
        }
    }
}
