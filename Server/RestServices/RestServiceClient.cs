using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Server.Utils;

namespace Server.RestServices
{
    internal class RestServiceClient : ClientBase<IRestService>, IRestService
    {
        public RestServiceClient(string url)
            : base("RestServiceClient", url)
        {
        }

        public RestServiceClient(IPEndPoint endPoint)
            : base("RestServiceClient", $"http://{endPoint}")
        {
        }

        public void Transfer(Transfer transfer)
        {
            using (new OperationContextScope(InnerChannel))
            {
                var messageProperty = new HttpRequestMessageProperty();
                messageProperty.Headers.Add(HttpRequestHeader.Authorization,
                    AuthorizationHeader.CreateAuthorizationHeader(ClientCredentials?.UserName.UserName,
                        ClientCredentials?.UserName.Password));

                OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, messageProperty);

                Channel.Transfer(transfer);
            }
        }
    }
}