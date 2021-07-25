using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.Kafka.Consumer.MessageServices;
using NetCore.Kafka.Consumer.Models;
using NetCore.Kafka.Platform.Consumer;
using NetCore.Kafka.Platform.Interfaces;
using NetCore.Kafka.Platform.Producer;

namespace NetCore.Kafka.Consumer
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }


        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var clientConfig = new ClientConfig()
                    {
                        BootstrapServers = Configuration["Kafka:ClientConfigs:BootstrapServers"]
                    };

                    var producerConfig = new ProducerConfig(clientConfig);
                    var consumerConfig = new ConsumerConfig(clientConfig)
                    {
                        GroupId = "SourceApp",
                        EnableAutoCommit = true,
                        AutoOffsetReset = AutoOffsetReset.Earliest,
                        StatisticsIntervalMs = 5000,
                        SessionTimeoutMs = 6000
                    };

                    services.AddSingleton(producerConfig);
                    services.AddSingleton(consumerConfig);

                    services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));

                    services.AddScoped<IKafkaHandler<string, OrderRequest>, PedidoRealizadoHandler>();
                    services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
                    services.AddHostedService<PedidoRealizadoConsumer>();
                });

            return host;
        }

    }
}
