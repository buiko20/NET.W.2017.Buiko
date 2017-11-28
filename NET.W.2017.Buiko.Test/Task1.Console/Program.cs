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
            var passwordCheckerService = new PasswordCheckerService();

            var result = passwordCheckerService.VerifyPassword("123123", repository, Verify);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword(string.Empty, repository, Verify);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword("123", repository, Verify);
            System.Console.WriteLine(result);

            result = passwordCheckerService.VerifyPassword("123456789e", repository, Verify);
            System.Console.WriteLine(result);

            System.Console.ReadLine();
        }

        private static Tuple<bool, string> Verify(string password)
        {
            // check if length more than 7 chars 
            if (password.Length <= 7)
                return Tuple.Create(false, $"{nameof(password)} length too short");

            // check if length more than 10 chars for admins
            if (password.Length >= 15)
                return Tuple.Create(false, $"{nameof(password)} length too long");

            // check if password conatins at least one alphabetical character 
            if (!password.Any(char.IsLetter))
                return Tuple.Create(false, $"{nameof(password)} hasn't alphanumerical chars");

            // check if password conatins at least one digit character 
            if (!password.Any(char.IsNumber))
                return Tuple.Create(false, $"{nameof(password)} hasn't digits");

            return new Tuple<bool, string>(true, "OK");
        }
    }
}
