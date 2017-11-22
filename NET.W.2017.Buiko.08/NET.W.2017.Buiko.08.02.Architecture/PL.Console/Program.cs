using System;
using System.IO;
using BLL.Interface.AccountService;
using DependencyResolver;
using Ninject;

namespace PL.Console
{
    internal class Program
    {
        private static readonly IKernel NinjectKernel;

        static Program()
        {
            NinjectKernel = new StandardKernel();
            NInjectDependencyResolver.Configure(NinjectKernel);   
        }

        private static void Main()
        {
            try
            {
               // var accountService = SimpleDependencyResolver.GetAccountService();
                var accountService = NinjectKernel.Get<IAccountService>();

                if (File.Exists(@"accounts.bin"))
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
            var id1 = accountService.OpenAccount("Name1", "Surname1", 100);
            System.Console.WriteLine("Account1 id: " + id1);
            accountService.DepositMoney(id1, 140);
            accountService.WithdrawMoney(id1, 40);
            System.Console.WriteLine($"{accountService.GetAccountStatus(id1)}  {accountService.GetAccountStatus(id1).Length}");

            var id2 = accountService.OpenAccount(AccountType.Platinum, "Name2", "Surname2", 444);
            System.Console.WriteLine("Account2 id: " + id2);
            accountService.DepositMoney(id2, 6);
            accountService.WithdrawMoney(id2, 40);
            System.Console.WriteLine($"{accountService.GetAccountStatus(id2)}  {accountService.GetAccountStatus(id2).Length}");
        }

        private static void Test(IAccountService accountService)
        {
            var id1 = "03rum5157S2e81n6291a";
            var id2 = "6r0ua08S0277832e1686";

            System.Console.WriteLine(accountService.GetAccountStatus(id1));
            System.Console.WriteLine(accountService.GetAccountStatus(id2));
        }
    }
}
