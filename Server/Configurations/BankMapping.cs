using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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

                var username = parts.ElementAtOrDefault(2) ?? "admin";
                var password = parts.ElementAtOrDefault(3) ?? "pass";

                var bankInfo = new BankInfo(parts[1], username, password);
                MappingsDictionary[parts[0]] = bankInfo;
            }
        }
    }
}