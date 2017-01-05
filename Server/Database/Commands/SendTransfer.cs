using System;
using System.Linq;
using Server.Database.Schema;
using Server.RestServices;
using Server.Utils;

namespace Server.Database.Commands
{
    public class SendTransfer
    {
        private readonly Transfer _transfer;
        private readonly string _username;

        public SendTransfer(Transfer transfer, string username)
        {
            _transfer = transfer;
            _username = username;
        }

        public void Execute(DatabaseDataContext context)
        {
            if (!_transfer.Amount.HasValue)
                throw new ArgumentNullException(nameof(_transfer.Amount), "Missing amount");

            if (_transfer.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(_transfer.Amount), "Value equal or less than zero");

            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _transfer.SenderAccount);
            if (account == null)
                throw new InvalidOperationException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new InvalidOperationException("Account doesn't belong to user");

            if (_transfer.Amount.Value > account.Balance)
                throw new InvalidOperationException("Amount is bigger than balance");

            account.Balance -= _transfer.Amount.Value;

            if (_transfer.ReceiverAccountNumber.IsLocalBank())
                DatabaseHandler.Execute(new ReceiveTransfer(_transfer));
            else
            {
                var bankInfo = _transfer.ReceiverAccountNumber.GetBankInfo();

                using (var client = new RestServiceClient(bankInfo.Url))
                {
                    client.ClientCredentials.UserName.UserName = bankInfo.Username;
                    client.ClientCredentials.UserName.Password = bankInfo.Password;
                    client.Transfer(_transfer);
                }
            }

            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _transfer.SenderAccount,
                Amount = -_transfer.Amount.Value,
                Result = account.Balance,
                Title = _transfer.Title,
                ConnectedAccount = _transfer.ReceiverAccount
            });
            context.SubmitChanges();
        }
    }
}