using Server.Database;
using Server.Database.Commands;
using Server.ServiceConfigurations;

namespace Server.SoapServices
{
    /// <summary>
    ///     SOAP service authorization manager
    /// </summary>
    public class SoapAuthorizationManager : AuthorizationManager
    {
        protected override bool ValidateLoginData(string username, string password)
        {
            return DatabaseHandler.Execute(new ValidateLoginData(username, password));
        }
    }
}