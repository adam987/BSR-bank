using System;
using System.Net;
using System.ServiceModel.Web;

namespace Server.Exceptions
{
    /// <summary>
    ///     Operation exception
    /// </summary>
    public class OperationException : Exception, ICustomException
    {
        /// <summary>
        ///     Operation exception constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public OperationException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Transfor exception to REST exception format
        /// </summary>
        /// <returns>REST exception</returns>
        public WebFaultException<ServiceExceptionBody> TransformToWebFaultException()
        {
            return new WebFaultException<ServiceExceptionBody>(new ServiceExceptionBody {Error = Message},
                HttpStatusCode.BadRequest);
        }
    }
}