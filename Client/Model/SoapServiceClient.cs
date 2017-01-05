using System.Collections.Generic;
using Common;

namespace Client.Model
{
    internal class SoapServiceClient : ClientBaseWithAuthorization<ISoapService>, ISoapService
    {
        public SoapServiceClient()
            : base("SoapServiceClient")
        {
        }

        public List<HistoryRow> GetHistory(string accountNumber)
            => SendRequest(() => Channel.GetHistory(accountNumber));

        public List<AccountRow> GetAccounts() => SendRequest(() => Channel.GetAccounts());

        public void Withdraw(string accountNumber, string title, decimal amount)
            => SendRequest(() => Channel.Withdraw(accountNumber, title, amount));

        public void Deposit(string accountNumber, string title, decimal amount)
            => SendRequest(() => Channel.Deposit(accountNumber, title, amount));

        public void Charge(string accountNumber, string title, decimal amount)
            => SendRequest(() => Channel.Charge(accountNumber, title, amount));
    }
}