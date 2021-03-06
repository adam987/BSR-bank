﻿using Server.Configurations;
using Server.ServiceConfigurations;

namespace Server.RestServices
{
    /// <summary>
    ///     REST service authorization manager
    /// </summary>
    public class RestAuthorizationManager : AuthorizationManager
    {
        private static BankInfo _bankInfo;

        private static BankInfo LocalBankInfo
            => _bankInfo = _bankInfo ?? BankMapping.Mappings[BankMapping.LocalBankNumber];

        protected override bool ValidateLoginData(string username, string password)
        {
            return (username == LocalBankInfo.Username) && (password == LocalBankInfo.Password);
        }
    }
}