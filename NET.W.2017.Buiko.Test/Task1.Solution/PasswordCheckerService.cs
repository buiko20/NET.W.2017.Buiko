using System;

namespace Task1.Solution
{
    public class PasswordCheckerService
    {
        public Tuple<bool, string> VerifyPassword(string password, IRepository repository, Func<string, Tuple<bool, string>> verifier)
        {
            VerifyInput(password, repository, verifier);

            if (string.Equals(password, string.Empty, StringComparison.Ordinal))
                return Tuple.Create(false, $"{nameof(password)} is empty ");

            var verification = verifier(password);
            if (!verification.Item1) return verification;

            repository.Create(password);

            return Tuple.Create(true, "Password is Ok. User was created");
        }

        private static void VerifyInput(string password, IRepository repository, Func<string, Tuple<bool, string>> verifier)
        {
            if (ReferenceEquals(password, null))
                throw new ArgumentException($"{nameof(password)} is null arg");

            if (ReferenceEquals(repository, null))
                throw new ArgumentNullException(nameof(repository));

            if (ReferenceEquals(verifier, null))
                throw new ArgumentNullException(nameof(verifier));
        }
    }
}
