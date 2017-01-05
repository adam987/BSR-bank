using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Server.Database.Schema;
using Server.Utils;

namespace Server.Configurations
{
    internal static class DatabaseAccounts
    {
        public static void Initialize()
        {
            AddCustomers();
            AddAccounts();
        }

        private static void AddCustomers()
        {
            using (var context = new DatabaseDataContext())
            {
                var file = new StreamReader("customers.txt");
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split(';').Select(part => part.Trim()).ToArray();
                    if (context.Customers.SingleOrDefault(customer => customer.Username == parts[0]) == null)
                        context.Customers.InsertOnSubmit(new Customer {Username = parts[0], Password = parts[1]});
                }
                context.SubmitChanges();
            }
        }

        private static void AddAccounts()
        {
            using (var context = new DatabaseDataContext())
            {
                var file = new StreamReader("accounts.txt");
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split(';').Select(part => part.Trim()).ToArray();
                    var accountNumber = new AccountNumber(parts[0]);
                    if (!accountNumber.ValidateAccountNumber())
                        throw new ArgumentException("Invalid account number");

                    var customerId = context.Customers.Single(customer => customer.Username == parts[2]).Id;
                    var balance = decimal.Parse(parts[1], CultureInfo.InvariantCulture);

                    if (context.Accounts.SingleOrDefault(account => account.Number == accountNumber.Number) == null)
                        context.Accounts.InsertOnSubmit(new Account
                        {
                            Number = accountNumber.Number,
                            Balance = balance,
                            Customer = customerId
                        });
                }
                context.SubmitChanges();
            }
        }
    }
}