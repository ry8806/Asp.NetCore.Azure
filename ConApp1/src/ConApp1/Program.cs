using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
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

            // Cloud Table Stuff
            CloudTableClient ctClient = acc.CreateCloudTableClient();
            CloudTable cloudTable = ctClient.GetTableReference("myAwesomeTable");

            // TODO: Create custom entities Implementing ITableEntity - much cleaner
            var ent = new DynamicTableEntity(Guid.NewGuid().ToString(), DateTime.Now.Ticks.ToString());
            ent.Properties.Add("FirstName", new EntityProperty("Ryan"));
            ent.Properties.Add("LastName", new EntityProperty("Southgate"));

            // Put in table
            var tblResult = cloudTable.ExecuteAsync(TableOperation.Insert(ent)).Result;

            // TODO: Properly await (in a non-console app)
            bool created = cloudTable.CreateIfNotExistsAsync().Result;

            Console.ReadLine();
        }
    }
}
