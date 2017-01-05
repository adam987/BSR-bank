using System.Runtime.Serialization;

namespace Server.SoapServices
{
    [DataContract]
    public class AccountRow
    {
        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public decimal Balance { get; set; }
    }
}