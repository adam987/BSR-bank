using System.Collections.Generic;
using System.ServiceModel;

namespace Server.SoapServices
{
    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        List<HistoryRow> GetHistory(string accountNumber);

        [OperationContract]
        List<AccountRow> GetAccounts();

        [OperationContract]
        void Withdraw(string accountNumber, string title, decimal amount);

        [OperationContract]
        void Deposit(string accountNumber, string title, decimal amount);

        [OperationContract]
        void Charge(string accountNumber, string title, decimal amount);
    }
}