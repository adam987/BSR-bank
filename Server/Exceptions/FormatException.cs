using System;
using System.Net;
using System.ServiceModel.Web;

namespace Server.Exceptions
{
    /// <summary>
    ///     Invalid message format exception
    /// </summary>
    public class FormatException : Exception, ICustomException
    {
        /// <summary>
        ///     Format exception constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public FormatException(string message)
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