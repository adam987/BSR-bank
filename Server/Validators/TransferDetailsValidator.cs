using System;
using Common.Contracts;
using Common.Utils;
using Server.ServiceConfigurations;
using Server.Utils;
using FormatException = Server.Exceptions.FormatException;

namespace Server.Validators
{
    /// <summary>
    ///     Transfor details validator
    /// </summary>
    public class TransferDetailsValidator : OperationBehaviourAttribute
    {
        /// <summary>
        ///     Validates transfor details
        /// </summary>
        /// <param name="operationName">operation name</param>
        /// <param name="inputs">input parameters</param>
        /// <returns>correlation state</returns>
        public override object BeforeCall(string operationName, object[] inputs)
        {
            if (inputs.Length != 1)
                throw new FormatException("Invalid parameters count");

            var transfer = inputs[0] as TransferDetails;

            if (transfer == null)
                throw new FormatException("Missing parameter");

            if (string.IsNullOrWhiteSpace(transfer.Title))
                throw new FormatException("Missing title");

            if (transfer.Title.Length >= 500)
                throw new FormatException("Title too long");

            decimal amount;

            try
            {
                amount = transfer.Amount.ToDecimal();
            }
            catch (Exception)
            {
                throw new FormatException("Invalid amount");
            }


            if (Math.Round(amount*100) != amount*100)
                throw new FormatException("Invalid amount");

            if (amount <= 0)
                throw new FormatException("Amount less than or equal to 0");

            if (amount > 1000000)
                throw new FormatException("Amount greater than 1.000.000");

            if (!AccountNumber.ValidateAccountNumber(transfer.ReceiverAccount))
                throw new FormatException("Invalid receiver account number");

            if (!AccountNumber.ValidateAccountNumber(transfer.SenderAccount))
                throw new FormatException("Invalid sender account number");

            if (transfer.ReceiverAccount == transfer.SenderAccount)
                throw new FormatException("Could not transfer to the same account");

            return null;
        }
    }
}