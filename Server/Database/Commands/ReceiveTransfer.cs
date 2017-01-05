using System;
using System.Linq;
using Server.Database.Schema;
using Server.Utils;

namespace Server.Database.Commands
{
    public class ReceiveTransfer : IDatabaseCommand
    {
        private readonly Transfer _transfer;

        public ReceiveTransfer(Transfer transfer)
        {
            _transfer = transfer;
        }

        public void Execute(DatabaseDataContext context)
        {
            if (!_transfer.Amount.HasValue)
                throw new ArgumentNullException(nameof(_transfer.Amount), "Missing amount");

            if (_transfer.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(_transfer.Amount), "Value equal or less than zero");

            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _transfer.ReceiverAccount);
            if (account == null)
                throw new InvalidOperationException("Account doesn't exist");

            account.Balance += _transfer.Amount.Value;
            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _transfer.ReceiverAccount,
                Amount = _transfer.Amount.Value,
                Result = account.Balance,
                Title = _transfer.Title,
                ConnectedAccount = _transfer.SenderAccount
            });
            context.SubmitChanges();
        }
    }
}