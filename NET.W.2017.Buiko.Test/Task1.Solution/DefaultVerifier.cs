using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Solution
{
    public class DefaultVerifier : IVerifier
    {
        public Tuple<bool, string> Verify(string password)
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
