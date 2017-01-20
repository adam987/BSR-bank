using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using Server.Exceptions;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Transforms not formatted exception to proper REST format
    /// </summary>
    public class JsonErrorHandler : IErrorHandler
    {
        /// <summary>
        ///     Is exception handled
        /// </summary>
        /// <param name="error">exception</param>
        /// <returns>is exception handled</returns>
        public bool HandleError(Exception error)
        {
            return true;
        }

        /// <summary>
        ///     Transforms exception to proper REST format
        /// </summary>
        /// <param name="error">exception</param>
        /// <param name="version">version</param>
        /// <param name="fault">return message</param>
        public void ProvideFault(Exception error, MessageVersion version,
            ref Message fault)
        {
            var exception = AdjustException((dynamic) error);

            fault = Message.CreateMessage(version, exception.Action, exception.Detail,
                new DataContractJsonSerializer(exception.Detail.GetType()));

            var jsonFormatting =
                new WebBodyFormatMessageProperty(WebContentFormat.Json);
            fault.Properties.Add(WebBodyFormatMessageProperty.Name, jsonFormatting);

            var httpResponse = new HttpResponseMessageProperty {StatusCode = exception.StatusCode};
            fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);
        }

        private static WebFaultException<ServiceExceptionBody> AdjustException(
            WebFaultException<ServiceExceptionBody> exception)
        {
            return exception;
        }

        private static WebFaultException<ServiceExceptionBody> AdjustException(Exception exception)
        {
            return (exception as ICustomException)?.TransformToWebFaultException() ??
                   new WebFaultException<ServiceExceptionBody>(new ServiceExceptionBody {Error = exception.Message},
                       HttpStatusCode.InternalServerError);
        }

        private static WebFaultException<ServiceExceptionBody> AdjustException(FaultException exception)
        {
            return new WebFaultException<ServiceExceptionBody>(new ServiceExceptionBody {Error = "Unauthorized"},
                HttpStatusCode.Unauthorized);
        }

        private static WebFaultException<ServiceExceptionBody> AdjustException(SerializationException exception)
        {
            return new WebFaultException<ServiceExceptionBody>(new ServiceExceptionBody {Error = exception.Message},
                HttpStatusCode.BadRequest);
        }
    }
}