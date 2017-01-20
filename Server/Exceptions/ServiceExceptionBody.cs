using System.Runtime.Serialization;

namespace Server.Exceptions
{
    /// <summary>
    ///     REST service exception body
    /// </summary>
    [DataContract]
    public class ServiceExceptionBody
    {
        /// <summary>
        ///     Error message
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}