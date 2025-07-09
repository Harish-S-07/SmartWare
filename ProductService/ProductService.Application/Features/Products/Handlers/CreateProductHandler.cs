using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductService.Application.DTO.Product;
using ProductService.Application.Features.Products.Commands;
using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;

namespace ProductService.Application.Features.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
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
