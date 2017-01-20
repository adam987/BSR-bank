using System.Runtime.Serialization;

namespace Common.Contracts
{
    /// <summary>
    ///     Single operation details
    /// </summary>
    [DataContract]
    public class OperationDetails
    {
        /// <summary>
        ///     Account number
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///     Amount
        /// </summary>
        [DataMember]
        public string Amount { get; set; }
    }
}