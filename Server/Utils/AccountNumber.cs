using System.Text.RegularExpressions;
using Server.Configurations;
using Server.Exceptions;

namespace Server.Utils
{
    /// <summary>
    ///     Account number tools
    /// </summary>
    public static class AccountNumber
    {
        /// <summary>
        ///     Validates account number
        /// </summary>
        /// <param name="number">account number</param>
        /// <returns>is account number valid</returns>
        public static bool ValidateAccountNumber(string number)
        {
            if (number?.Length != 26)
                return false;

            if (Regex.IsMatch(number, @"[^\d]"))
                return false;

            var wholeNumber = number.Substring(2) + $"{'P' - 'A' + 10}{'L' - 'A' + 10}" + number.Substring(0, 2);
            var rest = int.Parse(wholeNumber.Substring(0, 1));
            for (var i = 1; i < wholeNumber.Length; i++)
            {
                rest *= 10;
                rest += int.Parse(wholeNumber.Substring(i, 1));
                rest %= 97;
            }
            return rest == 1;
        }

        /// <summary>
        ///     Gets bank info for account number
        /// </summary>
        /// <param name="number">account number</param>
        /// <returns>bank info</returns>
        public static BankInfo GetBankInfo(string number)
        {
            try
            {
                return BankMapping.Mappings[number.Substring(2, 8)];
            }
            catch
            {
                throw new NotFoundException("Missing bank info");
            }
        }

        /// <summary>
        ///     Is account number from local bank
        /// </summary>
        /// <param name="number">account number</param>
        /// <returns>is account number from local bank</returns>
        public static bool IsLocalBank(string number)
        {
            return BankMapping.LocalBankNumber == number.Substring(2, 8);
        }
    }
}