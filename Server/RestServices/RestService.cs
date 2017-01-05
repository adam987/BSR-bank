using System;
using System.ServiceModel.Web;
using Server.Database;
using Server.Database.Commands;
using Server.Utils;

namespace Server.RestServices
{
    public class RestService : IRestService
    {
        private static OutgoingWebResponseContext Response => WebOperationContext.Current?.OutgoingResponse;

        public void Transfer(Transfer transfer)
        {
            try
            {
                DatabaseHandler.Execute(new ReceiveTransfer(transfer));
            }
            catch (Exception)
            {
            }
        }
    }
}