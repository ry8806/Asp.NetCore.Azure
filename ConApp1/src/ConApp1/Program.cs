using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json")
                                        .Build();

            var connectionString = config["Azure:ConnectionString"];

            CloudStorageAccount acc = CloudStorageAccount.Parse(connectionString);

            Console.ReadLine();
        }
    }
}
