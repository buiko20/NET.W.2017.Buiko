using BLL.Interface.AccountService;
using BLL.Services;
using DAL;
using DAL.Interface;
using Services.AccountIdService;
using Services.Interface.AccountIdService;
using Ninject;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IAccountRepository>().To<BinaryFileAccountRepository>()
                .WithConstructorArgument("dataFilePath", @"accounts.bin");

            kernel.Bind<IAccountIdService>().To<GuidAccountIdService>();

            var accountRepository = kernel.Get<IAccountRepository>();
            var accountIdService = kernel.Get<IAccountIdService>();
            kernel.Bind<IAccountService>().To<AccountService>()
                .WithConstructorArgument("accountRepository", accountRepository)
                .WithConstructorArgument("accountIdService", accountIdService);
        }
    }
}
