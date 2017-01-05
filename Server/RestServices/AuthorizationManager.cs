using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Server.Configurations;
using Server.Utils;

namespace Server.RestServices
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        private static BankInfo _bankInfo;
        private static IncomingWebRequestContext IncomingContext => WebOperationContext.Current?.IncomingRequest;
        private static OutgoingWebResponseContext OutgoingContext => WebOperationContext.Current?.OutgoingResponse;

        private static BankInfo LocalBankInfo
            => _bankInfo = _bankInfo ?? BankMapping.Mappings[BankMapping.LocalBankNumber];

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var authHeader = IncomingContext.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var credentials = AuthorizationHeader.GetCredentials(authHeader);
                if (credentials.Length == 2)
                {
                    if ((credentials[0] == LocalBankInfo.Username) && (credentials[1] == LocalBankInfo.Password))
                        return true;

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