using System.ServiceModel.Web;

namespace Server.Exceptions
{
    /// <summary>
    ///     To REST transformable exception
    /// </summary>
    public interface ICustomException
    {
        /// <summary>
        ///     Transfor exception to REST exception format
        /// </summary>
        /// <returns>REST exception</returns>
        WebFaultException<ServiceExceptionBody> TransformToWebFaultException();
    }
}