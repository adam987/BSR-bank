using System.Runtime.Serialization;

namespace Common.Contracts
{
    /// <summary>
    ///     Single account info
    /// </summary>
    [DataContract]
    public class AccountRow
    {
        /// <summary>
        ///     Account number
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }

        /// <summary>
        ///     Account balance
        /// </summary>
        [DataMember]
        public string Balance { get; set; }
    }
}