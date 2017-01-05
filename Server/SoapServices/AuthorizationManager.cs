using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Server.Database;
using Server.Utils;

namespace Server.SoapServices
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        private readonly IDatabaseHandler _databaseHandler = new DatabaseHandler();
        private static IncomingWebRequestContext IncomingContext => WebOperationContext.Current?.IncomingRequest;
        private static OutgoingWebResponseContext OutgoingContext => WebOperationContext.Current?.OutgoingResponse;

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var authHeader = IncomingContext.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var credentials = AuthorizationHeader.GetCredentials(authHeader);
                if (credentials.Length == 2)
                {
                    if (_databaseHandler.ValidateLoginDate(credentials[0], credentials[1]))
                    {
                        OperationContext.Current.RequestContext.RequestMessage.Properties.Add("Username", credentials[0]);
                        return true;
                    }

                    OutgoingContext.StatusCode = HttpStatusCode.Forbidden;
                    return false;
                }
            }

            OutgoingContext.Headers.Add("WWW-Authenticate", "Basic realm=\"Bank\"");
            OutgoingContext.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }
    }
}