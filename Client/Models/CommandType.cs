namespace Client.Models
{
    /// <summary>
    ///     Client operation type
    /// </summary>
    public enum CommandType
    {
        Charge,
        Withdraw,
        Deposit,
        History,
        Transfer,
        Refresh
    }
}