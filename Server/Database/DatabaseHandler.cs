using System;
using System.Collections.Generic;
using System.Linq;
using Server.Database.Schema;
using Server.RestServices;
using Server.SoapServices;
using Server.Utils;

namespace Server.Database
{
    internal class DatabaseHandler : IDatabaseHandler
    {
        private static readonly object LockObject = new object();

        public List<HistoryRow> GetHistory(AccountNumber accountNumber, string username)
        {
            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    var account = context.Accounts.SingleOrDefault(acc => acc.Number == accountNumber.Number);
                    if (account == null)
                        throw new InvalidOperationException("Account doesn't exist");

                    if (account.Customer1.Username != username)
                        throw new InvalidOperationException("Account doesn't belong to user");

                    return context.Histories.Where(history => history.AccountNumber == accountNumber.Number)
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

        public bool ValidateLoginDate(string username, string password)
        {
            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    return context.Customers.SingleOrDefault(customer =>
                                   (customer.Username == username) && (customer.Password == password)) != null;
                }
            }
        }

        public List<AccountRow> GetAccounts(string username)
        {
            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    return context.Accounts.Where(account => account.Customer1.Username == username)
                        .Select(row => new AccountRow {AccountNumber = row.Number, Balance = row.Balance})
                        .ToList();
                }
            }
        }

        public void Operation(AccountNumber accountNumber, string title, decimal amount, OperationType operationType,
            string username)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Value equal or less than zero");

            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    var account = context.Accounts.SingleOrDefault(acc => acc.Number == accountNumber.Number);
                    if (account == null)
                        throw new InvalidOperationException("Account doesn't exist");

                    if (account.Customer1.Username != username)
                        throw new InvalidOperationException("Account doesn't belong to user");

                    if ((operationType == OperationType.Withdraw) && (amount > account.Balance))
                        throw new InvalidOperationException("Amount is bigger than balance");

                    if (operationType != OperationType.Deposit)
                        amount = -amount;

                    account.Balance += amount;
                    context.Histories.InsertOnSubmit(new History
                    {
                        AccountNumber = accountNumber.Number,
                        Amount = amount,
                        Result = account.Balance,
                        Title = title,
                        OperationType = $"{operationType}".ToUpperInvariant()
                    });
                    context.SubmitChanges();
                }
            }
        }

        public void ReceiveTransfer(Transfer transfer)
        {
            if (!transfer.Amount.HasValue)
                throw new ArgumentNullException(nameof(transfer.Amount), "Missing amount");

            if (transfer.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(transfer.Amount), "Value equal or less than zero");

            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    var account = context.Accounts.SingleOrDefault(acc => acc.Number == transfer.ReceiverAccount);
                    if (account == null)
                        throw new InvalidOperationException("Account doesn't exist");

                    account.Balance += transfer.Amount.Value;
                    context.Histories.InsertOnSubmit(new History
                    {
                        AccountNumber = transfer.ReceiverAccount,
                        Amount = transfer.Amount.Value,
                        Result = account.Balance,
                        Title = transfer.Title,
                        ConnectedAccount = transfer.SenderAccount
                    });
                    context.SubmitChanges();
                }
            }
        }

        public void SendTransfer(Transfer transfer, string username)
        {
            if (!transfer.Amount.HasValue)
                throw new ArgumentNullException(nameof(transfer.Amount), "Missing amount");

            if (transfer.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(transfer.Amount), "Value equal or less than zero");

            lock (LockObject)
            {
                using (var context = new DatabaseMappingDataContext())
                {
                    var account = context.Accounts.SingleOrDefault(acc => acc.Number == transfer.SenderAccount);
                    if (account == null)
                        throw new InvalidOperationException("Account doesn't exist");

                    if (account.Customer1.Username != username)
                        throw new InvalidOperationException("Account doesn't belong to user");

                    if (transfer.Amount.Value > account.Balance)
                        throw new InvalidOperationException("Amount is bigger than balance");

                    account.Balance -= transfer.Amount.Value;

                    if (transfer.ReceiverAccountNumber.IsLocalBank())
                        ReceiveTransfer(transfer);
                    else
                    {
                        var bankInfo = transfer.ReceiverAccountNumber.GetBankInfo();

                        using (var client = new RestServiceClient(bankInfo.EndPoint))
                        {
                            if (client.ClientCredentials != null)
                            {
                                client.ClientCredentials.UserName.UserName = bankInfo.Username;
                                client.ClientCredentials.UserName.Password = bankInfo.Password;
                            }
                            client.Transfer(transfer);
                        }
                    }

                    context.Histories.InsertOnSubmit(new History
                    {
                        AccountNumber = transfer.SenderAccount,
                        Amount = -transfer.Amount.Value,
                        Result = account.Balance,
                        Title = transfer.Title,
                        ConnectedAccount = transfer.ReceiverAccount
                    });
                    context.SubmitChanges();
                }
            }
        }
    }
}