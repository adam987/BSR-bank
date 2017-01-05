using Server.Database;
using Server.Database.Commands;
using Server.Utils;

namespace Server.SoapServices
{
    public class SoapAuthorizationManager : AuthorizationManager
    {
        protected override bool ValidateLoginData(string username, string password)
        {
            return DatabaseHandler.Execute(new ValidateLoginData(username, password));
        }
    }
}