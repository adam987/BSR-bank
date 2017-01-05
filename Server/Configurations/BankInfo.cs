using System.Net;

namespace Server.Configurations
{
    public class BankInfo
    {
        public IPEndPoint EndPoint { get; }
        public string Username { get; }
        public string Password { get; }
        public string Url { get; }

        public BankInfo(IPEndPoint endPoint, string username, string password)
        {
            EndPoint = endPoint;
            Username = username;
            Password = password;
        }

        public BankInfo(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }
    }
}