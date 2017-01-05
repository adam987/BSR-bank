using System.Collections.Generic;
using System.Linq;
using Common;
using Server.Database.Schema;

namespace Server.Database.Commands
{
    public class GetAccounts : IDatabaseCommand<List<AccountRow>>
    {
        private readonly string _username;

        public GetAccounts(string username)
        {
            _username = username;
        }

        public List<AccountRow> Execute(DatabaseDataContext context)
        {
            return context.Accounts.Where(account => account.Customer1.Username == _username)
                .Select(row => new AccountRow {AccountNumber = row.Number, Balance = row.Balance})
                .ToList();
        }
    }
}