using System;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    ///     Authorization header handler
    /// </summary>
    public static class AuthorizationHeader
    {
        /// <summary>
        ///     Creates authorization header content
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>authorization header content</returns>
        public static string CreateAuthorizationHeader(string username, string password)
            => $"Basic {ConvertToBase64($"{username}:{password}")}";

        /// <summary>
        ///     Extracts credentials from header content
        /// </summary>
        /// <param name="header">header content</param>
        /// <returns>credentials</returns>
        public static string[] GetCredentials(string header) => ConvertFromBase64(header.Substring(6)).Split(':');

        private static string ConvertFromBase64(string header)
            => Encoding.ASCII.GetString(Convert.FromBase64String(header));

        private static string ConvertToBase64(string header)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(header));
    }
}