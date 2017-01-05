using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Server.Configurations;

namespace Server.Utils
{
    public class AccountNumber
    {
        public string Number { get; }

        public AccountNumber(string number)
        {
            Number = number;
        }

        public bool ValidateAccountNumber()
        {
            if (Number?.Length != 26)
                return false;

            if (Regex.IsMatch(Number, @"[^\d]"))
                return false;

            var number = Number.Substring(2) + $"{'P' - 'A' + 10}{'L' - 'A' + 10}" + Number.Substring(0, 2);
            var rest = 0;

            for (var i = 0; i < 30; i += 9)
            {
                var length = Math.Min(i + 9, 30) - i;
                rest = (int.Parse(number.Substring(i, length)) + rest)%97;
            }

            return rest == 1;
        }

        public BankInfo GetBankInfo()
        {
            try
            {
                return BankMapping.Mappings[Number.Substring(2, 8)];
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("Missing bank info");
            }
        }

        public bool IsLocalBank()
        {
            return BankMapping.LocalBankNumber == Number.Substring(2, 8);
        }
    }
}