using System;
using Common.Contracts;
using Common.Utils;
using Server.ServiceConfigurations;
using Server.Utils;
using FormatException = Server.Exceptions.FormatException;

namespace Server.Validators
{
    /// <summary>
    ///     Operation details validator
    /// </summary>
    public class OperationDetailsValidator : OperationBehaviourAttribute
    {
        /// <summary>
        ///     Validates operation details
        /// </summary>
        /// <param name="operationName">operation name</param>
        /// <param name="inputs">input parameters</param>
        /// <returns>correlation state</returns>
        public override object BeforeCall(string operationName, object[] inputs)
        {
            if (inputs.Length != 1)
                throw new FormatException("Invalid parameters count");

            var operationDetails = inputs[0] as OperationDetails;

            if (operationDetails == null)
                throw new FormatException("Missing parameter");

            if (string.IsNullOrWhiteSpace(operationDetails.Title))
                throw new FormatException("Missing title");

            if (operationDetails.Title.Length >= 500)
                throw new FormatException("Title too long");

            decimal amount;

            try
            {
                amount = operationDetails.Amount.ToDecimal();
            }
            catch (Exception)
            {
                throw new FormatException("Invalid amount");
            }

            if (amount <= 0)
                throw new FormatException("Amount less than or equal to 0");

            if (amount > 1000000)
                throw new FormatException("Amount greater than 1.000.000");

            if (!AccountNumber.ValidateAccountNumber(operationDetails.AccountNumber))
                throw new FormatException("Invalid account number");

            return null;
        }
    }
}