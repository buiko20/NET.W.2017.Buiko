using System.Data.Entity;
using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Interface.MailService;
using BLL.Services;
using DAL.BinaryFile;
using DAL.EF;
using DAL.Interface;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            //  kernel.Bind<IAccountRepository>().To<BinaryFileAccountRepository>()
            //      .WithConstructorArgument("dataFilePath", @"accounts.bin");

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();

            var dbContext = kernel.Get<DbContext>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>()
                .InSingletonScope()
                .WithConstructorArgument("dbContext", dbContext);

            kernel.Bind<IAccountIdService>().To<GuidAccountIdService>().InSingletonScope();

            kernel.Bind<IMailService>().To<GmailService>().InSingletonScope();

            var unitOfWork = kernel.Get<IUnitOfWork>();
            var accountRepository = kernel.Get<IAccountRepository>();
            var mailService = kernel.Get<IMailService>();
            kernel.Bind<IAccountService>().To<AccountService>().InSingletonScope()
                .WithConstructorArgument("unitOfWork", unitOfWork)
                .WithConstructorArgument("accountRepository", accountRepository)
                .WithConstructorArgument("mailService", mailService);
        }
    }
}
