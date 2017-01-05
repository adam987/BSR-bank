using System.Collections.Generic;
using Common;
using Server.Database;
using Server.Database.Commands;
using Server.Utils;

namespace Server.SoapServices
{
    public class SoapService : ISoapService
    {
        private static string Username => (string) WcfOperationContext.Current.Items["Username"];

        public List<HistoryRow> GetHistory(string accountNumber)
        {
            return DatabaseHandler.Execute(new GetHistory(new AccountNumber(accountNumber), Username));
        }

        public List<AccountRow> GetAccounts()
        {
            return DatabaseHandler.Execute(new GetAccounts(Username));
        }

        public void Withdraw(string accountNumber, string title, decimal amount)
        {
            DatabaseHandler.Execute(new Operation(new AccountNumber(accountNumber), title, amount,
                OperationType.Withdraw, Username));
        }

        public void Deposit(string accountNumber, string title, decimal amount)
        {
            DatabaseHandler.Execute(new Operation(new AccountNumber(accountNumber), title, amount, OperationType.Deposit,
                Username));
        }

        public void Charge(string accountNumber, string title, decimal amount)
        {
            DatabaseHandler.Execute(new Operation(new AccountNumber(accountNumber), title, amount, OperationType.Charge,
                Username));
        }
    }
}