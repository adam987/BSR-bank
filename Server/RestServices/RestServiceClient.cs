using Common;
using Server.Utils;

namespace Server.RestServices
{
    internal class RestServiceClient : ClientBaseWithAuthorization<IRestService>, IRestService
    {
        public RestServiceClient(string url)
            : base("RestServiceClient", url)
        {
        }

        public void Transfer(Transfer transfer)
        {
            SendRequest(() => Channel.Transfer(transfer));
        }
    }
}