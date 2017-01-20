using System.Linq;
using Server.Database.Schema;

namespace Server.Database.Commands
{
    /// <summary>
    ///     Validate login data command
    /// </summary>
    public class ValidateLoginData : IDatabaseCommand<bool>
    {
        private readonly string _password;
        private readonly string _username;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public ValidateLoginData(string username, string password)
        {
            _username = username;
            _password = password;
        }

        /// <summary>
        ///     Command executor
        /// </summary>
        /// <param name="context">database context</param>
        /// <returns>is login data valid</returns>
        public bool Execute(DatabaseDataContext context)
        {
            return context.Customers.SingleOrDefault(customer =>
                           (customer.Username == _username) && (customer.Password == _password)) != null;
        }
    }
}