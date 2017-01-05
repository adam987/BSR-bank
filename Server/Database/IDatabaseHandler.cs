using System.Collections.Generic;
using Server.SoapServices;
using Server.Utils;

namespace Server.Database
{
    internal interface IDatabaseHandler
    {
        List<HistoryRow> GetHistory(AccountNumber accountNumber, string username);
        bool ValidateLoginDate(string username, string password);
        List<AccountRow> GetAccounts(string username);

        void Operation(AccountNumber accountNumber, string title, decimal amount, OperationType operationType,
            string username);

        void ReceiveTransfer(Transfer transfer);
        void SendTransfer(Transfer transfer, string username);
    }
}