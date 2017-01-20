using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
    /// <summary>
    ///     Soap service contract
    /// </summary>
    [ServiceContract]
    public interface ISoapService
    {
        /// <summary>
        ///     Get account history
        /// </summary>
        /// <param name="accountNumber">account number</param>
        /// <returns>account history</returns>
        [OperationContract]
        List<HistoryRow> GetHistory(string accountNumber);

        /// <summary>
        ///     Get accounts list
        /// </summary>
        /// <returns>accounts list</returns>
        [OperationContract]
        List<AccountRow> GetAccounts();

        /// <summary>
        ///     Withdraw
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationContract]
        void Withdraw(OperationDetails operationDetails);

        /// <summary>
        ///     Deposit
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationContract]
        void Deposit(OperationDetails operationDetails);

        /// <summary>
        ///     Charge
        /// </summary>
        /// <param name="operationDetails">operation details</param>
        [OperationContract]
        void Charge(OperationDetails operationDetails);

        /// <summary>
        ///     Transfer
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        [OperationContract]
        void Transfer(TransferDetails transferDetails);
    }
}