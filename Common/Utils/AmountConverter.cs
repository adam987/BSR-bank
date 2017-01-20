namespace Common.Utils
{
    /// <summary>
    ///     Service amount to decimal converter
    /// </summary>
    public static class AmountConverter
    {
        /// <summary>
        ///     Converts to decimal
        /// </summary>
        /// <param name="amount">service string</param>
        /// <returns>decimal amount</returns>
        public static decimal ToDecimal(this string amount) => decimal.Parse(amount)/100;

        /// <summary>
        ///     Converts to service string
        /// </summary>
        /// <param name="amount">decimal amount</param>
        /// <returns>service string</returns>
        public static string ToServiceString(this decimal amount) => $"{(int) (amount*100)}";
    }
}