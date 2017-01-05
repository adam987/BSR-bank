using System.Linq;
using Server.Database.Schema;

namespace Server.Database.Commands
{
    public class ValidateLoginData : IDatabaseCommand<bool>
    {
        private readonly string _password;
        private readonly string _username;

        public ValidateLoginData(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public bool Execute(DatabaseDataContext context)
        {
            return context.Customers.SingleOrDefault(customer =>
                           (customer.Username == _username) && (customer.Password == _password)) != null;
        }
    }
}