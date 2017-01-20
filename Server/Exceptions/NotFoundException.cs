using System;
using System.Net;
using System.ServiceModel.Web;

namespace Server.Exceptions
{
    /// <summary>
    ///     Not found exception
    /// </summary>
    public class NotFoundException : Exception, ICustomException
    {
        /// <summary>
        ///     Not found exception constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public NotFoundException(string message)
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
                HttpStatusCode.NotFound);
        }
    }
}