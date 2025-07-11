using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryService.Application.Common.Interfaces;
using InventoryService.Domain.Entities;
using MongoDB.Driver;

namespace InventoryService.Infrastructure.Persistence
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly MongoDbContext _dbContext;
        public InventoryRepository(MongoDbContext mongoDbContext) 
        {
            _dbContext = mongoDbContext;
        }

        public async Task AddAsync(Inventory inventory)
        {
            await _dbContext.Inventories.InsertOneAsync(inventory);
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _dbContext.Inventories.Find(_ => true).ToListAsync();
        }

        public async Task<Inventory?> GetByProductIdAsync(Guid productId)
        {
            return await _dbContext.Inventories.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
