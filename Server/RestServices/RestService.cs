using System.Net;
using System.ServiceModel.Web;
using Common.Contracts;
using Server.Database;
using Server.Database.Commands;
using Server.Validators;

namespace Server.RestServices
{
    /// <summary>
    ///     REST service implementation
    /// </summary>
    public class RestService : IRestService
    {
        private static OutgoingWebResponseContext Response => WebOperationContext.Current?.OutgoingResponse;

        /// <summary>
        ///     Transfer operation
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        [TransferDetailsValidator]
        public void Transfer(TransferDetails transferDetails)
        {
            DatabaseHandler.Execute(new ReceiveTransfer(transferDetails));
            Response.StatusCode = HttpStatusCode.Created;
        }
    }
}