using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Service behaviour attribute for exception validation
    /// </summary>
    public abstract class ServiceBehaviourAttribute : Attribute, IServiceBehavior, IErrorHandler
    {
        /// <summary>
        ///     Transforms message to proper format
        /// </summary>
        /// <param name="error">exception</param>
        /// <param name="version">version</param>
        /// <param name="fault">return message</param>
        public virtual void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }

        /// <summary>
        ///     Is exception handled
        /// </summary>
        /// <param name="error">exception</param>
        /// <returns>is exception handled</returns>
        public virtual bool HandleError(Exception error)
        {
            return true;
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="serviceDescription">service description</param>
        /// <param name="serviceHostBase">service host base</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        ///     Not used
        /// </summary>
        /// <param name="serviceDescription">service description</param>
        /// <param name="serviceHostBase">service host base</param>
        /// <param name="endpoints">endpoints</param>
        /// <param name="bindingParameters">binding parameters</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        ///     Adds custom error handler
        /// </summary>
        /// <param name="serviceDescription">service description</param>
        /// <param name="serviceHostBase">service host base</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var dispatcher in serviceHostBase.ChannelDispatchers)
                ((ChannelDispatcher) dispatcher).ErrorHandlers.Add(this);
        }
    }
}