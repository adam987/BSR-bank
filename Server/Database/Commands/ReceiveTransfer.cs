using System;
using System.Linq;
using Common.Contracts;
using Common.Utils;
using Server.Database.Schema;
using Server.Exceptions;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Receive transfer command
    /// </summary>
    public class ReceiveTransfer : IDatabaseCommand
    {
        private readonly TransferDetails _transferDetails;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        public ReceiveTransfer(TransferDetails transferDetails)
        {
            _transferDetails = transferDetails;
        }

        /// <summary>
        ///     Command executor for remote transfer
        /// </summary>
        /// <param name="context">database context</param>
        public void Execute(DatabaseDataContext context) => Execute(context, true);

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        /// <param name="remoteCall">is it remote call</param>
        public void Execute(DatabaseDataContext context, bool remoteCall)
        {
            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _transferDetails.ReceiverAccount);
            if (account == null)
                throw new NotFoundException("Account doesn't exist");

            var amount = _transferDetails.Amount.ToDecimal();
            account.Balance += amount;
            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _transferDetails.ReceiverAccount,
                Amount = amount,
                Result = account.Balance,
                Title = _transferDetails.Title,
                ConnectedAccount = _transferDetails.SenderAccount,
                OperationType = OperationType.Transfer.ToString().ToUpperInvariant(),
                Date = DateTime.Now
            });

            if (remoteCall)
                context.SubmitChanges();
        }
    }
}