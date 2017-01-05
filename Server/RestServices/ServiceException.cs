using System.Runtime.Serialization;

namespace Server.RestServices
{
    [DataContract]
    internal class ServiceException
    {
        [DataMember]
        public string Error { get; set; }
    }
}