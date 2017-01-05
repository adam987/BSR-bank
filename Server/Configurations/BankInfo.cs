namespace Server.Configurations
{
    public class BankInfo
    {
        public string Username { get; }
        public string Password { get; }
        public string Url { get; }

        public BankInfo(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }
    }
}