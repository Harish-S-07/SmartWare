using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.DTO.Product;

namespace ProductService.Application.Features.Products.Commands
{
    public class CreateProductCommand
    {
        public CreateProductDTO Product { get; set; }

        public CreateProductCommand(CreateProductDTO product)
        {
            Product = product;
        }
    }
}
