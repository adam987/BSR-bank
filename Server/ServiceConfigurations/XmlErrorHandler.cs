using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     SOAP error handler
    /// </summary>
    public class XmlErrorHandler : ServiceBehaviourAttribute
    {
        /// <summary>
        ///     Transforms message to proper SOAP format
        /// </summary>
        /// <param name="error">exception</param>
        /// <param name="version">version</param>
        /// <param name="fault">return message</param>
        public override void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var exception = error as FaultException ?? new FaultException(error.Message);
            fault = Message.CreateMessage(version, exception.CreateMessageFault(), string.Empty);
        }
    }
}