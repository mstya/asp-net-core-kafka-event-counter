using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace KafkaLib
{
    public class KafkaConsumer : IDisposable
    {
        private readonly Consumer<string, string> consumer;

        public KafkaConsumer(Dictionary<string, object> config)
        {
            this.consumer = new Consumer<string, string>(config, new StringDeserializer(Encoding.UTF8), new StringDeserializer(Encoding.UTF8));
        }

        public void Consume(Func<string, Task> callback, string topic, string key, int partitionIndex, CancellationToken cancellationToken)
        {
            Console.WriteLine("Topic: {0}, PartitionIndex: {1}", topic, partitionIndex);
            this.consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, partitionIndex, new Offset(-1))  });

            consumer.OnMessage += (_, msg) =>
            {
                callback(msg.Value);
                consumer.CommitAsync(msg);
            };

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                consumer.Poll(100);
            }
        }

        public void Dispose()
        {
            this.consumer.Dispose();
        }
    }
}