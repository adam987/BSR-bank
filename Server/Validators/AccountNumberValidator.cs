using Server.Exceptions;
using Server.ServiceConfigurations;
using Server.Utils;

namespace Server.Validators
{
    /// <summary>
    ///     Account number validator
    /// </summary>
    public class AccountNumberValidator : OperationBehaviourAttribute
    {
        /// <summary>
        ///     Validates account number
        /// </summary>
        /// <param name="operationName">operation name</param>
        /// <param name="inputs">input parameters</param>
        /// <returns>correlation state</returns>
        public override object BeforeCall(string operationName, object[] inputs)
        {
            if (inputs.Length != 1)
                throw new FormatException("Invalid parameters count");

            var accountNumber = inputs[0] as string;

            if (accountNumber == null)
                throw new FormatException("Missing parameter");

            if (!AccountNumber.ValidateAccountNumber(accountNumber))
                throw new FormatException("Invalid account number");

            return null;
        }
    }
}