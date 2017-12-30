using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using BLL.Services.Account_Service;
using BLL.Services.AccountIdService;
using BLL.Services.MailService;
using DAL;
using DAL.Interface;
using Ninject;
using ORM;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<DbContext>().To<BankContext>().InSingletonScope();
            kernel.Bind<IAccountIdService>().To<GuidAccountIdService>().InSingletonScope();
            kernel.Bind<IMailService>().To<GmailService>().InSingletonScope();

            var dbContext = kernel.Get<DbContext>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope()
                .WithConstructorArgument("dbContext", dbContext);

            kernel.Bind<IBankUserRepository>().To<BankUserRepository>().InSingletonScope()
                .WithConstructorArgument("dbContext", dbContext);

            var accountRepository = kernel.Get<IAccountRepository>();
            var accountIdService = kernel.Get<IAccountIdService>();
            kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope()
                .WithConstructorArgument("accountRepository", accountRepository)
                .WithConstructorArgument("accountIdService", accountIdService);

            var mailService = kernel.Get<IMailService>();
            var unitOfWork = kernel.Get<IUnitOfWork>();            
            var userRepository = kernel.Get<IBankUserRepository>();
            var accountService = kernel.Get<IAccountService>();
            kernel.Bind<IBankService>().To<BankService>().InSingletonScope()
                .WithConstructorArgument("mailService", mailService)
                .WithConstructorArgument("unitOfWork", unitOfWork)
                .WithConstructorArgument("userRepository", userRepository)
                .WithConstructorArgument("accountService", accountService);
        }
    }
}
