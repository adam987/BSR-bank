using System.Runtime.Serialization;

namespace Common.Contracts
{
    /// <summary>
    ///     Single transfer details
    /// </summary>
    [DataContract]
    public class TransferDetails
    {
        /// <summary>
        ///     Amount
        /// </summary>
        [DataMember(Name = "amount")]
        public string Amount { get; set; }

        /// <summary>
        ///     Sender account
        /// </summary>
        [DataMember(Name = "sender_account")]
        public string SenderAccount { get; set; }

        /// <summary>
        ///     Receiver account
        /// </summary>
        [DataMember(Name = "receiver_account")]
        public string ReceiverAccount { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}