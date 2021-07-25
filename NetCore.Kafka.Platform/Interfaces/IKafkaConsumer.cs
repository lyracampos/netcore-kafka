using System.Threading;
using System.Threading.Tasks;

namespace NetCore.Kafka.Platform.Interfaces
{
    public interface IKafkaConsumer<TKey, TValue> where TValue : class
    {
        Task Consume(string topic, CancellationToken stoppingToken);
        void Close();
        void Dispose();
    }
}