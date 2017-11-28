using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Solution;

namespace Task1.Console
{
    internal class Program
    {
        private static void Main()
        {
            var repository = new SqlRepository();
            var verifier = new DefaultVerifier();
            var passwordCheckerService = new PasswordCheckerService();

            var result = passwordCheckerService.VerifyPassword("123123", repository, verifier);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword("", repository, verifier);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword("123", repository, verifier);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword("123456789e", repository, verifier);
            System.Console.WriteLine(result);

            System.Console.ReadLine();
        }
    }
}
