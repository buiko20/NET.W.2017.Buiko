using System;
using System.IO;
using Logic.AccountCreationService;
using Logic.AccountCreationService.Implementation;
using Logic.AccountIdGeneratorService;
using Logic.AccountIdGeneratorService.Implementation;
using Logic.AccountRepository;
using Logic.AccountRepository.Implementation;
using Logic.AccountService;
using Logic.AccountService.Implementation;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                IAccountRepository accountRepository = new BinaryFileAccountRepository(@"accounts.bin");
                AccountIdService accountIdService = new GuidAccountIdService();
                IAccountCreater accountCreater = new AccountCreater();

                IAccountService accountService = new AccountService(accountRepository, accountIdService, accountCreater);

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
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void FileInitTests(IAccountService accountService)
        {
            var id1 = accountService.OpenAccount("Name1", "Surname1", 100);
            Console.WriteLine("Account1 id: " + id1);
            accountService.DepositMoney(id1, 140);
            accountService.WithdrawMoney(id1, 40);
            Console.WriteLine($"{accountService.GetAccountStatus(id1)}  {accountService.GetAccountStatus(id1).Length}");

            var id2 = accountService.OpenAccount("Name2", "Surname2", 444);
            Console.WriteLine("Account2 id: " + id2);
            accountService.DepositMoney(id2, 6);
            accountService.WithdrawMoney(id2, 40);
            Console.WriteLine($"{accountService.GetAccountStatus(id2)}  {accountService.GetAccountStatus(id2).Length}");
        }

        private static void Test(IAccountService accountService)
        {
            string id1 = "e22682u5809n11Srm278";
            string id2 = "S016rm2551a987une227";

            Console.WriteLine(accountService.GetAccountStatus(id1));
            Console.WriteLine(accountService.GetAccountStatus(id2));
        }
    }
}
