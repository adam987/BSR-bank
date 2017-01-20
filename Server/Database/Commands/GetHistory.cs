using System;
using System.Collections.Generic;
using System.Linq;
using Common.Contracts;
using Common.Utils;
using Server.Database.Schema;
using Server.Exceptions;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Get history for account command
    /// </summary>
    public class GetHistory : IDatabaseCommand<List<HistoryRow>>
    {
        private readonly string _accountNumber;
        private readonly string _username;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <param name="username">username</param>
        public GetHistory(string accountNumber, string username)
        {
            _accountNumber = accountNumber;
            _username = username;
        }

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        /// <returns>history rows list</returns>
        public List<HistoryRow> Execute(DatabaseDataContext context)
        {
            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _accountNumber);
            if (account == null)
                throw new NotFoundException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new NotFoundException("Account doesn't belong to user");

            return context.Histories.Where(history => history.AccountNumber == _accountNumber)
                .ToList()
                .Select(row =>
                    new HistoryRow
                    {
                        Amount = row.Amount.ToServiceString(),
                        Title = row.Title,
                        Result = row.Result.ToServiceString(),
                        Date = row.Date,
                        OperationType = (OperationType) Enum.Parse(typeof(OperationType), row.OperationType, true),
                        ConnectedAccount = row.ConnectedAccount
                    })
                .ToList();
        }
    }
}