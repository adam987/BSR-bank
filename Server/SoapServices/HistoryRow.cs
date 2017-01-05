using System;
using System.Runtime.Serialization;
using Server.Database;

namespace Server.SoapServices
{
    [DataContract]
    public class HistoryRow
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal Result { get; set; }

        [DataMember]
        public OperationType? OperationType { get; set; }

        [DataMember]
        public string ConnectedAccount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}