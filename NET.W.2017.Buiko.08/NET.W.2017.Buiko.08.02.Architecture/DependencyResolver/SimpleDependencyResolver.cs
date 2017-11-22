using BLL.Interface.AccountService;
using BLL.Services;
using DAL;
using Services.AccountIdService;

namespace DependencyResolver
{
    public static class SimpleDependencyResolver
    {
        private const string DataFilePath = @"accounts.bin";

        public static IAccountService GetAccountService()
        {
            var accountRepository = new BinaryFileAccountRepository(DataFilePath);
            var accountIdService = new GuidAccountIdService();
            return new AccountService(accountRepository, accountIdService);
        }
    }
}
