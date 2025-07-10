using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using ProductService.Application.Interface;

namespace ProductService.Infrastructure.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig _config;
        public KafkaProducer(IConfiguration configuration)
        {
            _config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);
            try
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
                Console.WriteLine($"Message '{jsonMessage}' sent to topic '{result.Topic}' at partition {result.Partition} with offset {result.Offset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Failed to deliver message: {e.Error.Reason}");
            }
        }
    }
}
