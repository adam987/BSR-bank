using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Server.Database.Schema;
using Server.Utils;

namespace Server.Database.Commands
{
    public class GetHistory : IDatabaseCommand<List<HistoryRow>>
    {
        private readonly AccountNumber _accountNumber;
        private readonly string _username;

        public GetHistory(AccountNumber accountNumber, string username)
        {
            _accountNumber = accountNumber;
            _username = username;
        }

        public List<HistoryRow> Execute(DatabaseDataContext context)
        {
            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _accountNumber.Number);
            if (account == null)
                throw new InvalidOperationException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new InvalidOperationException("Account doesn't belong to user");

            return context.Histories.Where(history => history.AccountNumber == _accountNumber.Number)
                .Select(row =>
                    new HistoryRow
                    {
                        Amount = row.Amount,
                        Title = row.Title,
                        Result = row.Result,
                        Date = row.Date,
                        OperationType = row.OperationType != null
                            ? (OperationType?) Enum.Parse(typeof(OperationType), row.OperationType)
                            : null,
                        ConnectedAccount = row.ConnectedAccount
                    })
                .ToList();
        }
    }
}