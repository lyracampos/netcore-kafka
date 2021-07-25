using System.Threading.Tasks;
using NetCore.Kafka.Consumer.Models;
using NetCore.Kafka.Platform.Interfaces;
using System;
using System.Linq;

namespace NetCore.Kafka.Consumer.MessageServices
{
    public class PedidoRealizadoHandler : IKafkaHandler<string, OrderRequest>
	{
		private readonly IKafkaProducer<string, OrderRequest> _producer;

		public PedidoRealizadoHandler(IKafkaProducer<string, OrderRequest> producer)
		{
			_producer = producer;
		}
		public Task HandleAsync(string key, OrderRequest value)
		{
            Console.WriteLine($"Consumindo pedido com itens: {value.Itens.Count()}");
			// Here we can actually write the code to register a User
	
			//After successful operation, suppose if the registered user has User Id as 1 the we can produce message for other service's consumption
			//_producer.ProduceAsync(KafkaTopics.UserRegistered, "", new UserRegistered { UserId = 1 });

			return Task.CompletedTask;
		}
    }
}