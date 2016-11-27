using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Server.RestServices;
using Server.SoapServices;

namespace Server
{
    internal class Program
    {
        private static readonly List<Type> Services = new List<Type> {typeof(SoapService), typeof(RestService)};

        private static void Main(string[] args)
        {
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