using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using InventoryService.Application.Common.Interfaces;
using InventoryService.Domain.Entities;
using InventoryService.Infrastructure.Kafka.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryService.Infrastructure.Kafka.Consumers
{
    public class ProductCreatedConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public ProductCreatedConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "inventory-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("product-created");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    var message = JsonSerializer.Deserialize<ProductCreatedEvent>(result.Message.Value);

                    if (message != null)
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var repo = scope.ServiceProvider.GetRequiredService<IInventoryRepository>();

                        var inventory = new Inventory
                        {
                            Id = Guid.NewGuid(),
                            ProductId = message.ProductId,
                            Name = message.Name,
                            Quantity = message.Quantity
                        };

                        await repo.AddAsync(inventory);
                        Console.WriteLine($"Inventory created for Product: {message.Name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Kafka consume error: {ex.Message}");
                }
            }
        }

    }
}
