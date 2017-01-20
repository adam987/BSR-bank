using System;
using System.Linq;
using Common.Contracts;
using Common.Utils;
using Server.Database.Schema;
using Server.Exceptions;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Operation commmand (deposit, withdraw, charge)
    /// </summary>
    public class Operation : IDatabaseCommand
    {
        private readonly OperationDetails _details;
        private readonly OperationType _operationType;
        private readonly string _username;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="details">operation details</param>
        /// <param name="operationType">operation type</param>
        /// <param name="username">username</param>
        public Operation(OperationDetails details, OperationType operationType, string username)
        {
            _details = details;
            _operationType = operationType;
            _username = username;
        }

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        public void Execute(DatabaseDataContext context)
        {
            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _details.AccountNumber);
            if (account == null)
                throw new NotFoundException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new NotFoundException("Account doesn't belong to user");

            var amount = _details.Amount.ToDecimal();
            if ((_operationType == OperationType.Withdraw) && (amount > account.Balance))
                throw new OperationException("Amount is bigger than balance");

            amount = _operationType == OperationType.Deposit ? amount : -amount;

            try
            {
                account.Balance += amount;
            }
            catch (Exception e)
            {
                throw new OperationException(e.Message);
            }

            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _details.AccountNumber,
                Amount = amount,
                Result = account.Balance,
                Title = _details.Title,
                OperationType = $"{_operationType}".ToUpperInvariant(),
                Date = DateTime.Now
            });
            context.SubmitChanges();
        }
    }
}