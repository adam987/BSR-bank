using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace Server.Configurations
{
    internal static class BankMapping
    {
        private static readonly Dictionary<string, BankInfo> MappingsDictionary = new Dictionary<string, BankInfo>();

        public static string LocalBankNumber;

        public static IReadOnlyDictionary<string, BankInfo> Mappings
            => new ReadOnlyDictionary<string, BankInfo>(MappingsDictionary);

        public static void Initialize()
        {
            var file = new StreamReader("banks.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var parts = line.Split(';').Select(part => part.Trim()).ToArray();
                if (LocalBankNumber == null)
                    LocalBankNumber = parts[0];

                var addressParts = parts[1].Split(':');

                IPAddress ipAddress;
                var username = parts.ElementAtOrDefault(2) ?? "admin";
                var password = parts.ElementAtOrDefault(3) ?? "admin";

                var bankInfo = IPAddress.TryParse(addressParts[0], out ipAddress)
                    ? new BankInfo(new IPEndPoint(ipAddress, int.Parse(addressParts[1])), username, password)
                    : new BankInfo(parts[1], username, password);
                MappingsDictionary[parts[0]] = bankInfo;
            }
        }
    }
}