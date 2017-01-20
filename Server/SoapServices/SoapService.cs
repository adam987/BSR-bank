using System.Collections.Generic;
using Common.Contracts;
using Server.Database;
using Server.Database.Commands;
using Server.ServiceConfigurations;
using Server.Utils;
using Server.Validators;

namespace Server.SoapServices
{
    /// <summary>
    ///     SOAP service implementation
    /// </summary>
    [XmlErrorHandler]
    public class SoapService : ISoapService
    {
        private static string Username => (string) WcfOperationContext.Current.Items["Username"];

        /// <summary>
        ///     Get history operation
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns>history rows list</returns>
        [AccountNumberValidator]
        public List<HistoryRow> GetHistory(string accountNumber)
        {
            return DatabaseHandler.Execute(new GetHistory(accountNumber, Username));
        }

        /// <summary>
        ///     Get accounts operation
        /// </summary>
        /// <returns>accounts list</returns>
        public List<AccountRow> GetAccounts()
        {
            return DatabaseHandler.Execute(new GetAccounts(Username));
        }

        /// <summary>
        ///     Transfer operation
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        [TransferDetailsValidator]
        public void Transfer(TransferDetails transferDetails)
        {
            DatabaseHandler.Execute(new SendTransfer(transferDetails, Username));
        }

        /// <summary>
        ///     Withdraw operation
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationDetailsValidator]
        public void Withdraw(OperationDetails operationDetails)
        {
            DatabaseHandler.Execute(new Operation(operationDetails, OperationType.Withdraw, Username));
        }

        /// <summary>
        ///     Deposit operation
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationDetailsValidator]
        public void Deposit(OperationDetails operationDetails)
        {
            DatabaseHandler.Execute(new Operation(operationDetails, OperationType.Deposit, Username));
        }

        /// <summary>
        ///     Charge operation
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationDetailsValidator]
        public void Charge(OperationDetails operationDetails)
        {
            DatabaseHandler.Execute(new Operation(operationDetails, OperationType.Charge, Username));
        }
    }
}