using System;
using System.ServiceModel;
using Server.RestServices;
using Server.SoapServices;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var soapService = new ServiceHost(typeof(SoapService)))
            {
                using (var restService = new ServiceHost(typeof(RestService)))
                {
                    Console.WriteLine("Starting soap service");
                    soapService.Open();
                    Console.WriteLine("Starting rest service");
                    restService.Open();
                    Console.WriteLine("Press ENTER to stop server...");
                    Console.ReadLine();
                    Console.WriteLine("Stopping server");
                    soapService.Close();
                }
            }
        }
    }
}