using System.ServiceModel.Web;
using Server.Database;
using Server.Utils;

namespace Server.RestServices
{
    public class RestService : IRestService
    {
        private readonly IDatabaseHandler _database = new DatabaseHandler();
        private static OutgoingWebResponseContext Response => WebOperationContext.Current?.OutgoingResponse;

        public void Transfer(Transfer transfer)
        {
            try
            {
                _database.ReceiveTransfer(transfer);
            }
            catch
            {
                
            }
        }
    }
}