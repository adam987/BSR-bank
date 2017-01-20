using System;
using System.Runtime.Serialization;

namespace Common.Contracts
{
    /// <summary>
    ///     Single history operation info
    /// </summary>
    [DataContract]
    public class HistoryRow
    {
        /// <summary>
        ///     Operation title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///     Operation amount
        /// </summary>
        [DataMember]
        public string Amount { get; set; }

        /// <summary>
        ///     Operation result balance
        /// </summary>
        [DataMember]
        public string Result { get; set; }

        /// <summary>
        ///     Operation type
        /// </summary>
        [DataMember]
        public OperationType OperationType { get; set; }

        /// <summary>
        ///     Transfer referred account
        /// </summary>
        [DataMember]
        public string ConnectedAccount { get; set; }

        /// <summary>
        ///     Operation date
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }
    }
}