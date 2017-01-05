using System.Collections.Generic;
using System.ServiceModel;
using Server.Database;
using Server.Utils;

namespace Server.SoapServices
{
    public class SoapService : ISoapService
    {
        private readonly IDatabaseHandler _database = new DatabaseHandler();

        private static string Username
        {
            get
            {
                object username;
                OperationContext.Current.RequestContext.RequestMessage.Properties.TryGetValue("Username", out username);
                return (string) username;
            }
        }

        public List<HistoryRow> GetHistory(string accountNumber)
        {
            return _database.GetHistory(new AccountNumber(accountNumber), Username);
        }

        public List<AccountRow> GetAccounts()
        {
            return _database.GetAccounts(Username);
        }

        public void Withdraw(string accountNumber, string title, decimal amount)
        {
            _database.Operation(new AccountNumber(accountNumber), title, amount, OperationType.Withdraw, Username);
        }

        public void Deposit(string accountNumber, string title, decimal amount)
        {
            _database.Operation(new AccountNumber(accountNumber), title, amount, OperationType.Deposit, Username);
        }

        public void Charge(string accountNumber, string title, decimal amount)
        {
            _database.Operation(new AccountNumber(accountNumber), title, amount, OperationType.Charge, Username);
        }
    }
}