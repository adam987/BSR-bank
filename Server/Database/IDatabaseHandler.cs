using System.Collections.Generic;

namespace Server.Database
{
    internal interface IDatabaseHandler
    {
        bool Operation(string accountNumber, decimal amount);
        List<HistoryRow> GetHistory(string accountNumber);
        bool? ValidateLoginDate(string username, string password);
        decimal? GetBalance(string accountNumber);
    }
}