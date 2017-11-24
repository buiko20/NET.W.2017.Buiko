using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Services;
using DAL;
using DAL.Interface;
using Ninject;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IAccountRepository>().To<BinaryFileAccountRepository>()
                .WithConstructorArgument("dataFilePath", @"accounts.bin");

            kernel.Bind<IAccountIdService>().To<GuidAccountIdService>().InSingletonScope();

            var accountRepository = kernel.Get<IAccountRepository>();
            kernel.Bind<IAccountService>().To<AccountService>()
                .WithConstructorArgument("accountRepository", accountRepository);
        }
    }
}
