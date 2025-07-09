using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductService.Application.DTO.Category;

namespace ProductService.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDTO>
    {
        public CreateCategoryDTO Category { get; set; }

        public CreateCategoryCommand(CreateCategoryDTO category)
        {
            Category = category;
        }
    }
}
