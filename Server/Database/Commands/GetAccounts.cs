using System.Collections.Generic;
using System.Linq;
using Common.Contracts;
using Common.Utils;
using Server.Database.Schema;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Get accounts for user command
    /// </summary>
    public class GetAccounts : IDatabaseCommand<List<AccountRow>>
    {
        private readonly string _username;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="username">username</param>
        public GetAccounts(string username)
        {
            _username = username;
        }

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        /// <returns>accounts list</returns>
        public List<AccountRow> Execute(DatabaseDataContext context)
        {
            return context.Accounts.Where(account => account.Customer1.Username == _username)
                .ToList()
                .Select(row => new AccountRow {AccountNumber = row.Number, Balance = row.Balance.ToServiceString()})
                .ToList();
        }
    }
}