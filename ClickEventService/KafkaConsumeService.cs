using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KafkaLib;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace ClickEventService
{
    public class KafkaConsumeService : IHostedService
    {
        private readonly IHubContext<ServiceHub> hub;
        private readonly KafkaConsumer consumer;

        private readonly Dictionary<string, object> config = new Dictionary<string, object>
        {
            {"group.id", "click-event-service-group"},
            {"bootstrap.servers", "localhost:9092"},
            {"enable.auto.commit", "true"},
            { "client.id", "event-counter" },
        };

        public static string Key { get; set; }

        public static int PartitionIndex { get; set; }

        public KafkaConsumeService(IHubContext<ServiceHub> hub)
        {
            this.hub = hub;

            this.consumer = new KafkaConsumer(this.config);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service started");

            consumer.Consume(Handle, Topics.PostClickEventTopic, Key, PartitionIndex, cancellationToken);

            return Task.CompletedTask;
        }

        public async Task Handle(string data)
        {
            Console.WriteLine(data);

            await this.hub.Clients.All.InvokeAsync("GetClickEvent", data);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stopped");

            this.consumer.Dispose();

            return Task.CompletedTask;
        }
    }
}