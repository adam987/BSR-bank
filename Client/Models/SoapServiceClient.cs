using System.Collections.Generic;
using Common.Contracts;
using Common.Utils;

namespace Client.Models
{
    /// <summary>
    ///     Service client implementation
    /// </summary>
    internal class SoapServiceClient : ClientBaseWithAuthorization<ISoapService>, ISoapService
    {
        /// <summary>
        ///     Constructor with default configuration
        /// </summary>
        public SoapServiceClient()
            : base("SoapServiceClient")
        {
        }

        /// <summary>
        ///     Get history
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns></returns>
        public List<HistoryRow> GetHistory(string accountNumber)
            => SendRequest(() => Channel.GetHistory(accountNumber));

        /// <summary>
        ///     Get accounts
        /// </summary>
        /// <returns>accounts list</returns>
        public List<AccountRow> GetAccounts() => SendRequest(() => Channel.GetAccounts());

        /// <summary>
        ///     Withdraw
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        public void Withdraw(OperationDetails operationDetails)
            => SendRequest(() => Channel.Withdraw(operationDetails));

        /// <summary>
        ///     Deposit
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        public void Deposit(OperationDetails operationDetails)
            => SendRequest(() => Channel.Deposit(operationDetails));

        /// <summary>
        ///     Charge
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        public void Charge(OperationDetails operationDetails)
            => SendRequest(() => Channel.Charge(operationDetails));

        /// <summary>
        ///     Transfer
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        public void Transfer(TransferDetails transferDetails)
            => SendRequest(() => Channel.Transfer(transferDetails));
    }
}