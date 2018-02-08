using System.Threading.Tasks;

namespace KafkaLib
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(string topic, string key, string data, int partition);
    }
}