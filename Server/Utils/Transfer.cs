using System.Runtime.Serialization;

namespace Server.Utils
{
    [DataContract]
    public class Transfer
    {
        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        [DataMember(Name = "sender_account")]
        public string SenderAccount { get; set; }

        [DataMember(Name = "receiver_account")]
        public string ReceiverAccount { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [IgnoreDataMember]
        public AccountNumber SenderAccountNumber => new AccountNumber(SenderAccount);

        [IgnoreDataMember]
        public AccountNumber ReceiverAccountNumber => new AccountNumber(ReceiverAccount);
    }
}