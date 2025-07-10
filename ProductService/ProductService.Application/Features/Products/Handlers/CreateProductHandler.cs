using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductService.Application.Common.Events;
using ProductService.Application.DTO.Product;
using ProductService.Application.Features.Products.Commands;
using ProductService.Application.Interface;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Application.Features.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _repository;

        private readonly IKafkaProducer _kafkaProducer;

        public CreateProductHandler(IProductRepository repository, IKafkaProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = command.Product.Name,
                Description = command.Product.Description,
                Price = command.Product.Price,
                Quantity = command.Product.Quantity,
                CategoryId = command.Product.CategoryId
            };

            await _repository.AddAsync(entity);

            var @event = new ProductCreatedEvent
            {
                ProductId = entity.Id,
                Name = entity.Name,
                Quantity = entity.Quantity,
            };

            await _kafkaProducer.PublishAsync("product-created", @event);

            return new ProductDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Quantity = entity.Quantity,
                CategoryId = entity.CategoryId
            };
        }
    }
}
