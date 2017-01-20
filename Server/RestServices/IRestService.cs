using System.ServiceModel;
using System.ServiceModel.Web;
using Common.Contracts;
using Server.Exceptions;

namespace Server.RestServices
{
    /// <summary>
    ///     REST service contract
    /// </summary>
    [ServiceContract]
    public interface IRestService
    {
        /// <summary>
        ///     Transfer operation
        /// </summary>
        /// <param name="transferDetails">transfer details</param>
        [OperationContract]
        [WebInvoke(UriTemplate = "transfer", Method = "POST", ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(ServiceExceptionBody))]
        void Transfer(TransferDetails transferDetails);
    }
}