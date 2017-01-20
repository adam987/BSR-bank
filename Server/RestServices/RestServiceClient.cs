using Common.Contracts;
using Common.Utils;

namespace Server.RestServices
{
    /// <summary>
    ///     REST service client
    /// </summary>
    internal class RestServiceClient : ClientBaseWithAuthorization<IRestService>, IRestService
    {
        /// <summary>
        ///     REST service client constructor with default configuration
        /// </summary>
        /// <param name="url">REST server url</param>
        public RestServiceClient(string url)
            : base("RestServiceClient", url)
        {
        }

        /// <summary>
        ///     Transfer to remote REST server operation
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        public void Transfer(TransferDetails transferDetails)
        {
            SendRequest(() => Channel.Transfer(transferDetails));
        }
    }
}