using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Server.SoapServices;
using Server.Utils;

namespace Client.Model
{
    internal class SoapServiceClient : ClientBase<ISoapService>, ISoapService
    {
        public SoapServiceClient(string url = "http://127.0.0.1:12001")
            : base("SoapServiceClient", url)
        {
        }

        public List<HistoryRow> GetHistory(string accountNumber)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                return Channel.GetHistory(accountNumber);
            }
        }

        public List<AccountRow> GetAccounts()
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                return Channel.GetAccounts();
            }
        }

        public void Withdraw(string accountNumber, string title, decimal amount)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                Channel.Withdraw(accountNumber, title, amount);
            }
        }

        public void Deposit(string accountNumber, string title, decimal amount)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                Channel.Deposit(accountNumber, title, amount);
            }
        }

        public void Charge(string accountNumber, string title, decimal amount)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                Channel.Charge(accountNumber, title, amount);
            }
        }

        private void AddAuthorizationHeader()
        {
            var messageProperty = new HttpRequestMessageProperty();
            messageProperty.Headers.Add(HttpRequestHeader.Authorization,
                AuthorizationHeader.CreateAuthorizationHeader(ClientCredentials?.UserName.UserName,
                    ClientCredentials?.UserName.Password));

            OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, messageProperty);
        }
    }
}