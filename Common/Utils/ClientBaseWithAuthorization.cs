using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Common.Utils
{
    /// <summary>
    ///     Service client with basic authorization implementation
    /// </summary>
    /// <typeparam name="TChannel">service contract</typeparam>
    public abstract class ClientBaseWithAuthorization<TChannel> : ClientBase<TChannel> where TChannel : class
    {
        protected ClientBaseWithAuthorization(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        protected ClientBaseWithAuthorization(string endpointConfigurationName, string url)
            : base(endpointConfigurationName, url)
        {
        }

        protected void AddAuthorizationHeader()
        {
            if (string.IsNullOrEmpty(ClientCredentials?.UserName.UserName) ||
                string.IsNullOrEmpty(ClientCredentials?.UserName.Password))
                return;

            var messageProperty = new HttpRequestMessageProperty();
            messageProperty.Headers.Add(HttpRequestHeader.Authorization,
                AuthorizationHeader.CreateAuthorizationHeader(ClientCredentials?.UserName.UserName,
                    ClientCredentials?.UserName.Password));

            OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, messageProperty);
        }

        protected T SendRequest<T>(Func<T> func)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                return func();
            }
        }

        protected void SendRequest(Action action)
        {
            using (new OperationContextScope(InnerChannel))
            {
                AddAuthorizationHeader();
                action();
            }
        }
    }
}