using System;
using System.Linq;
using Common;
using Server.Database.Schema;
using Server.Utils;

namespace Server.Database.Commands
{
    public class Operation : IDatabaseCommand
    {
        private readonly AccountNumber _accountNumber;
        private readonly decimal _amount;
        private readonly OperationType _operationType;
        private readonly string _title;
        private readonly string _username;

        public Operation(AccountNumber accountNumber, string title, decimal amount, OperationType operationType,
            string username)
        {
            _accountNumber = accountNumber;
            _title = title;
            _amount = amount;
            _operationType = operationType;
            _username = username;
        }

        public void Execute(DatabaseDataContext context)
        {
            if (_amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(_amount), "Value equal or less than zero");

            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _accountNumber.Number);
            if (account == null)
                throw new InvalidOperationException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new InvalidOperationException("Account doesn't belong to user");

            if ((_operationType == OperationType.Withdraw) && (_amount > account.Balance))
                throw new InvalidOperationException("Amount is bigger than balance");

            var amount = _operationType == OperationType.Deposit ? _amount : -_amount;

            account.Balance += amount;
            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _accountNumber.Number,
                Amount = amount,
                Result = account.Balance,
                Title = _title,
                OperationType = $"{_operationType}".ToUpperInvariant()
            });
            context.SubmitChanges();
        }
    }
}