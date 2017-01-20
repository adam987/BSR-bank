using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Common.Utils;
using Server.Utils;

namespace Server.ServiceConfigurations
{
    /// <summary>
    ///     Service authorization manager base implementation
    /// </summary>
    public abstract class AuthorizationManager : ServiceAuthorizationManager
    {
        private static IncomingWebRequestContext IncomingContext => WebOperationContext.Current?.IncomingRequest;
        private static OutgoingWebResponseContext OutgoingContext => WebOperationContext.Current?.OutgoingResponse;

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            try
            {
                var authHeader = IncomingContext.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader))
                {
                    var credentials = AuthorizationHeader.GetCredentials(authHeader);
                    if (credentials.Length == 2)
                        try
                        {
                            if (ValidateLoginData(credentials[0], credentials[1]))
                            {
                                WcfOperationContext.Current.Items.Add("Username", credentials[0]);
                                return true;
                            }
                        }
                        catch (Exception)
                        {
                            OutgoingContext.StatusCode = HttpStatusCode.InternalServerError;
                            return false;
                        }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            OutgoingContext.Headers.Add("WWW-Authenticate", "Basic realm=\"Bank\"");
            OutgoingContext.StatusCode = HttpStatusCode.Unauthorized;
            return false;
        }

        protected abstract bool ValidateLoginData(string username, string password);
    }
}