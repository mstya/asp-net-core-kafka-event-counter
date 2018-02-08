using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace KafkaLib
{
    public class KafkaConsumeService<THub> : IHostedService
        where THub : Hub
    {
        private readonly IHubContext<THub> hub;

        private readonly KafkaConsumer consumer;

        private readonly string topic;

        public static string Key { get; set; }

        public static int PartitionIndex { get; set; }

        public KafkaConsumeService(IHubContext<THub> hub, Dictionary<string, object> config, string topic)
        {
            this.hub = hub;
            this.topic = topic;
            this.consumer = new KafkaConsumer(config);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started");

            consumer.Consume(Handle, this.topic, Key, PartitionIndex, cancellationToken);

            return Task.CompletedTask;
        }

        public async Task Handle(string data)
        {
            Console.WriteLine(data);

            await this.hub.Clients.All.InvokeAsync("OnEvent", data);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stopped");

            this.consumer.Dispose();

            return Task.CompletedTask;
        }
    }
}