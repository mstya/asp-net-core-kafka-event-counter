using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace KafkaLib
{
    public class KafkaProducer : IKafkaProducer, IDisposable
    {
        private readonly Producer<string, string> producer;

        public KafkaProducer(Dictionary<string, object> config)
        {
            producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));
        }

        public Task ProduceAsync(string topic, string key, string data, int partition)
        {
            return this.producer.ProduceAsync(topic, key, data, partition);
        }

        public void Dispose()
        {
            this.producer.Dispose();
        }
    }
}