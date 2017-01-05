using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Server.SoapServices;

namespace ConsoleClient
{
    class Program
    {
        internal static class AuthorizationHeader
        {
            public static string CreateAuthorizationHeader(string username, string password)
                => $"Basic {ConvertToBase64($"{username}:{password}")}";

            public static string[] GetCredentials(string header) => ConvertFromBase64(header.Substring(6)).Split(':');

            private static string ConvertFromBase64(string header)
                => Encoding.ASCII.GetString(Convert.FromBase64String(header));

            private static string ConvertToBase64(string header)
                => Convert.ToBase64String(Encoding.ASCII.GetBytes(header));
        }

        public class SoapClient : ClientBase<ISoapService>, ISoapService
        {
            public SoapClient(string url)
                : base("SoapServiceClient", url)
            {
                
            }

            public List<HistoryRow> GetHistory(string accountNumber)
            {
                return Channel.GetHistory(accountNumber);
            }

            public List<AccountRow> GetAccounts()
            {
                using (new OperationContextScope(InnerChannel))
                {
                    var messageProperty = new HttpRequestMessageProperty();
                    messageProperty.Headers.Add(HttpRequestHeader.Authorization,
                        AuthorizationHeader.CreateAuthorizationHeader(ClientCredentials?.UserName.UserName,
                            ClientCredentials?.UserName.Password));

                    OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, messageProperty);

                    return Channel.GetAccounts();
                }       
            }

            public void Withdraw(string accountNumber, string title, decimal amount)
            {
                Channel.Withdraw(accountNumber, title, amount);
            }

            public void Deposit(string accountNumber, string title, decimal amount)
            {
                Channel.Deposit(accountNumber, title, amount);
            }

            public void Charge(string accountNumber, string title, decimal amount)
            {
                Channel.Charge(accountNumber, title, amount);
            }
        }

        static void Main(string[] args)
        {
            using (var client = new SoapClient("http://127.0.0.1:12001"))
            {
                client.ClientCredentials.UserName.UserName = "user1";
                client.ClientCredentials.UserName.Password = "pass1";

                var accounts = client.GetAccounts();
            }
        }
    }
}
