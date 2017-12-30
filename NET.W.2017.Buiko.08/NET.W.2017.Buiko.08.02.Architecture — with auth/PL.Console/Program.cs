using System;
using BLL.Interface.AccountIdService;
using BLL.Interface.AccountService;
using BLL.Interface.MailService;
using DependencyResolver;
using Ninject;

namespace PL.Console
{
    internal class Program
    {
        private static readonly IAccountIdService AccountIdService;
        private static readonly IKernel NinjectKernel;

        static Program()
        {
            NinjectKernel = new StandardKernel();
            NInjectDependencyResolver.Configure(NinjectKernel);  
            AccountIdService = NinjectKernel.Get<IAccountIdService>(); 
        }

        private static void Main()
        {
            try
            {
                // var accountService = SimpleDependencyResolver.GetAccountService();
                var accountService = NinjectKernel.Get<IAccountService>();

                if (true)
                {
                    Test(accountService);
                }
                else
                {
                    FileInitTests(accountService);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            System.Console.ReadLine();
        }

        private static void FileInitTests(IAccountService accountService)
        {
            var id1 = accountService.OpenAccount("Name1", "Surname1", 100, "email1@mail.ru", AccountIdService);
            System.Console.WriteLine("Account1 id: " + id1);
            accountService.DepositMoney(id1, 140);
            accountService.WithdrawMoney(id1, 40);
            System.Console.WriteLine($"{accountService.GetAccountStatus(id1)}  {accountService.GetAccountStatus(id1).Length}");

            var id2 = accountService.OpenAccount(AccountType.Platinum, "Name2", "Surname2", 444, "email2@mail.ru", AccountIdService);
            System.Console.WriteLine("Account2 id: " + id2);
            accountService.DepositMoney(id2, 6);
            accountService.WithdrawMoney(id2, 40);
            System.Console.WriteLine($"{accountService.GetAccountStatus(id2)}  {accountService.GetAccountStatus(id2).Length}");
        }

        private static void Test(IAccountService accountService)
        {
            var id1 = "27nSmre2118au2077645";
            var id2 = "r58956n08e8ua0643S12";

            System.Console.WriteLine(accountService.GetAccountStatus(id1));
            System.Console.WriteLine(accountService.GetAccountStatus(id2));
        }
    }
}
