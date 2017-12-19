using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Interface.MailService;
using BLL.Services;
using DAL.BinaryFile;
using DAL.EF;
using DAL.Interface;
using Ninject;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            //  kernel.Bind<IAccountRepository>().To<BinaryFileAccountRepository>()
            //      .WithConstructorArgument("dataFilePath", @"accounts.bin");

            kernel.Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope();

            kernel.Bind<IAccountIdService>().To<GuidAccountIdService>().InSingletonScope();

            var accountRepository = kernel.Get<IAccountRepository>();
            kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope()
                .WithConstructorArgument("accountRepository", accountRepository);

            kernel.Bind<IMailService>().To<GmailService>().InSingletonScope();
        }
    }
}
