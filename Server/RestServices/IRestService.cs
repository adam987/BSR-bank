using System.ServiceModel;
using System.ServiceModel.Web;
using Server.Utils;

namespace Server.RestServices
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "transfer", Method = "POST")]
        void Transfer(Transfer transfer);
    }
}