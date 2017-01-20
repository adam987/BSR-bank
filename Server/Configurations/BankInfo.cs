namespace Server.Configurations
{
    /// <summary>
    ///     Basic bank info
    /// </summary>
    public class BankInfo
    {
        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; }

        /// <summary>
        ///     Password
        /// </summary>
        public string Password { get; }

        /// <summary>
        ///     Bank url
        /// </summary>
        public string Url { get; }

        /// <summary>
        ///     Bank info constructor
        /// </summary>
        /// <param name="url">bank url</param>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        public BankInfo(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }
    }
}