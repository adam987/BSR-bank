using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Server.Configurations;
using Server.RestServices;
using Server.SoapServices;

namespace Server
{
    internal class Program
    {
        private static readonly List<Type> Services = new List<Type> {typeof(SoapService), typeof(RestService)};

        private static void Main(string[] args)
        {
            Console.WriteLine("Initialiazing...");
            DatabaseAccounts.Initialize();
            BankMapping.Initialize();
            Console.WriteLine("Initialized");

            var serviceHosts = Services.Select(service =>
            {
                Console.WriteLine($"Starting {service.Name}");
                var serviceHost = new ServiceHost(service);
                serviceHost.Open();
                return serviceHost;
            }).ToList();

            Console.WriteLine("Press ENTER to stop server...");
            Console.ReadLine();
            Console.WriteLine("Stopping server");

            serviceHosts.ForEach(serviceHost => serviceHost.Close());
        }
    }
}