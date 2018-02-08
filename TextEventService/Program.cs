using System;
using KafkaLib;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TextEventService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            string port = "41003";
            int partitionIndex = 0;
            if (args.Length == 2)
            {
                port = args[0];
                partitionIndex = int.Parse(args[1]);
            }

            string url = "http://10.10.41.235:" + port;
            KafkaConsumeService<ServiceHub>.PartitionIndex = partitionIndex;

            Console.WriteLine("Text event service {0}", partitionIndex);
            Console.WriteLine("URL: " + url);
            Console.WriteLine("Partition index: " + KafkaConsumeService<ServiceHub>.PartitionIndex);
            Console.WriteLine("***********************************************************");
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();
        }
    }
}