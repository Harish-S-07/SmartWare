using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryService.Domain.Entities;

namespace InventoryService.Application.Common.Interfaces
{
    public interface IInventoryRepository
    {
        Task AddAsync(Inventory inventory);
        Task<List<Inventory>> GetAllAsync();
        Task<Inventory?> GetByProductIdAsync(Guid productId);
    }
}
