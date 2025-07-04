using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.DTO.Product;

namespace ProductService.Application.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(Guid id);
        Task<ProductDTO> CreateProductAsync(ProductDTO ProductDTO);
        Task<ProductDTO> UpdateProductAsync(ProductDTO ProductDTO);
        Task DeleteProductAsync(Guid id);
    }
}
