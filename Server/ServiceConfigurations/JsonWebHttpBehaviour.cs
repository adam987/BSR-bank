using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Custom web http behaviour without default handlers
    /// </summary>
    public class JsonWebHttpBehaviour : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new JsonErrorHandler());
        }
    }
}