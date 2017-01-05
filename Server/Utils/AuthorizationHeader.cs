using System;
using System.Text;

namespace Server.Utils
{
    public static class AuthorizationHeader
    {
        public static string CreateAuthorizationHeader(string username, string password)
            => $"Basic {ConvertToBase64($"{username}:{password}")}";

        public static string[] GetCredentials(string header) => ConvertFromBase64(header.Substring(6)).Split(':');

        private static string ConvertFromBase64(string header)
            => Encoding.ASCII.GetString(Convert.FromBase64String(header));

        private static string ConvertToBase64(string header)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(header));
    }
}