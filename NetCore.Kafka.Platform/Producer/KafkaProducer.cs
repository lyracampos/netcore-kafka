using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using NetCore.Kafka.Platform.Helpers;
using NetCore.Kafka.Platform.Interfaces;

namespace NetCore.Kafka.Platform.Producer
{
    public class KafkaProducer<TKey, TValue> : IDisposable, IKafkaProducer<TKey, TValue> where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<TKey, TValue>(config).SetValueSerializer(new KafkaSerializer<TValue>()).Build();
        }
        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }

        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
        }
    }
}