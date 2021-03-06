﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Logic.AccountRepository.Exceptions;
using Logic.Domain.Account;

namespace Logic.AccountRepository.Implementation
{
    /// <inheritdoc />
    public class BinaryFileAccountRepository : IAccountRepository
    {
        #region private fields

        private readonly string _dataFilePath;
        private readonly List<Account> _accounts = new List<Account>();

        #endregion // !private fields.

        #region public

        #region constructor

        /// <summary>
        /// Initializes the instance of repository with account information.
        /// </summary>
        public BinaryFileAccountRepository(string dataFilePath)
        {
            if (string.IsNullOrWhiteSpace(dataFilePath))
            {
                throw new ArgumentException(nameof(dataFilePath));
            }

            _dataFilePath = dataFilePath;

            try
            {
                if (File.Exists(_dataFilePath))
                {
                    ParseFile(_dataFilePath);
                }
            }
            catch (Exception)
            {
                _accounts.Clear();
            }
        }

        #endregion // !constructor.

        #region interfaces implementation

        /// <inheritdoc />
        public void AddAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (_accounts.Any(account.Equals))
            {
                throw new RepositoryException("This account already exists");
            }

            try
            {
                AppendAccountToFile(account);
                _accounts.Add(account);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Add account error", e);
            }
        }

        /// <inheritdoc />
        public Account GetAccount(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            return _accounts.FirstOrDefault(account => account.Id == id);
        }

        /// <inheritdoc />
        public void UpdateAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new RepositoryException("Account does not exists");
            }

            try
            {
                _accounts.Remove(account);
                _accounts.Add(account);
                WriteAccountsToFile(_accounts);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Update account error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveAccount(Account account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new RepositoryException("Aaccount does not exists");
            }

            try
            {
                _accounts.Remove(account);
                WriteAccountsToFile(_accounts);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Remove account error", e);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Account> GetAccounts() =>
            _accounts.ToArray();

        #endregion // !interfaces implementation.

        #endregion // !public.

        #region private

        private static Account ReadAccountFromFile(BinaryReader binaryReader)
        {
            string typeName = binaryReader.ReadString();
            var accountType = Type.GetType(typeName);

            string id = binaryReader.ReadString();
            string ownerFirstName = binaryReader.ReadString();
            string ownerSecondName = binaryReader.ReadString();
            decimal sum = binaryReader.ReadDecimal();
            int bonusPoints = binaryReader.ReadInt32();

            return CreateAccount(accountType, id, ownerFirstName, ownerSecondName, sum, bonusPoints);
        }

        private static void WriteAccountToFile(BinaryWriter binaryWriter, Account account)
        {
            binaryWriter.Write(account.GetType().ToString());
            binaryWriter.Write(account.Id);
            binaryWriter.Write(account.OwnerFirstName);
            binaryWriter.Write(account.OwnerSecondName);
            binaryWriter.Write(account.CurrentSum);
            binaryWriter.Write(account.BonusPoints);
        }

        private static Account CreateAccount(
            Type accountType, string id, string onwerFirstName, string onwerSecondName, decimal sum, int bonusPoints) 
        {
            return Activator.CreateInstance(accountType, id, onwerFirstName, onwerSecondName, sum, bonusPoints) as Account;
        }

        private void ParseFile(string filePath)
        {
            using (var binaryReader = new BinaryReader(
                File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8, false))
            {
                while (binaryReader.PeekChar() > -1)
                {
                    var account = ReadAccountFromFile(binaryReader);
                    _accounts.Add(account);
                }
            }
        }

        private void AppendAccountToFile(Account account)
        {
            using (var binaryWriter = new BinaryWriter(
                File.Open(_dataFilePath, FileMode.Append, FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                WriteAccountToFile(binaryWriter, account);
            }
        }

        private void WriteAccountsToFile(IEnumerable<Account> accounts)
        {
            using (var binaryWriter = new BinaryWriter(
                File.Open(_dataFilePath, FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                foreach (var account in accounts)
                {
                    WriteAccountToFile(binaryWriter, account);
                }
            }
        }

        #endregion // !private.
    }
}
