using System;
using System.Linq;
using System.Net;
using Common.Contracts;
using Common.Utils;
using Server.Database.Schema;
using Server.Exceptions;
using Server.RestServices;
using Server.Utils;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Send transfer command
    /// </summary>
    public class SendTransfer : IDatabaseCommand
    {
        private readonly TransferDetails _transferDetails;
        private readonly string _username;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        /// <param name="username">username</param>
        public SendTransfer(TransferDetails transferDetails, string username)
        {
            _transferDetails = transferDetails;
            _username = username;
        }

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        public void Execute(DatabaseDataContext context)
        {
            var account = context.Accounts.SingleOrDefault(acc => acc.Number == _transferDetails.SenderAccount);
            if (account == null)
                throw new NotFoundException("Account doesn't exist");

            if (account.Customer1.Username != _username)
                throw new NotFoundException("Account doesn't belong to user");

            var amount = _transferDetails.Amount.ToDecimal();
            if (amount > account.Balance)
                throw new OperationException("Amount is bigger than balance");

            try
            {
                account.Balance -= amount;
            }
            catch (Exception e)
            {
                throw new OperationException(e.Message);
            }

            if (AccountNumber.IsLocalBank(_transferDetails.ReceiverAccount))
                new ReceiveTransfer(_transferDetails).Execute(context, false);
            else
            {
                var bankInfo = AccountNumber.GetBankInfo(_transferDetails.ReceiverAccount);

                try
                {
                    using (var client = new RestServiceClient(bankInfo.Url))
                    {
                        client.ClientCredentials.UserName.UserName = bankInfo.Username;
                        client.ClientCredentials.UserName.Password = bankInfo.Password;
                        client.Transfer(_transferDetails);
                    }
                }
                catch (Exception ex)
                {
                    var webException = ex.InnerException as WebException;
                    if (webException?.Response == null)
                        throw new OperationException("Could not transfer to remote bank");

                    ServiceExceptionBody body;
                    HttpStatusCode statusCode;

                    try
                    {
                        var response = (HttpWebResponse) webException.Response;
                        statusCode = response.StatusCode;
                        body = response.GetBody<ServiceExceptionBody>();
                    }
                    catch
                    {
                        throw new OperationException("Invalid remote server response");
                    }

                    throw new OperationException(
                        $"Remote server returned: {statusCode}. Message: {body.Error}");
                }
            }

            context.Histories.InsertOnSubmit(new History
            {
                AccountNumber = _transferDetails.SenderAccount,
                Amount = -amount,
                Result = account.Balance,
                Title = _transferDetails.Title,
                ConnectedAccount = _transferDetails.ReceiverAccount,
                OperationType = OperationType.Transfer.ToString().ToUpperInvariant(),
                Date = DateTime.Now
            });
            context.SubmitChanges();
        }
    }
}