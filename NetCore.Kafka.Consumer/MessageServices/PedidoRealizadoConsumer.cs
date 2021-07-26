using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetCore.Kafka.Consumer.Models;
using NetCore.Kafka.Messages.Topics;
using NetCore.Kafka.Platform.Interfaces;

namespace NetCore.Kafka.Consumer.MessageServices
{
    public class PedidoRealizadoConsumer : BackgroundService
	{
		private readonly IKafkaConsumer<string, OrderRequest> _consumer;
		public PedidoRealizadoConsumer(IKafkaConsumer<string, OrderRequest> kafkaConsumer)
		{
			_consumer = kafkaConsumer;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				await _consumer.Consume(OrderTopics.CreateOrder, stoppingToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic {ex}");
			}
		}

		public override void Dispose()
		{
			_consumer.Close();
			_consumer.Dispose();

			base.Dispose();
		}
	}
}