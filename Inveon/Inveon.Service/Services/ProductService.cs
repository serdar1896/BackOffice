using Inveon.Core.Interfaces.CacheManagement;
using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inveon.Core.Models.Entities;

namespace Inveon.Service.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IRedisManagement _redisManager;

        public ProductService(IBaseRepository<Product> repository,IRedisManagement redisManager) : base(repository)
        {
            _redisManager = redisManager;
        }
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            var cacheResult = await _redisManager.GetAsync<List<Product>>($"productAll");
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var productList = await base.GetAllAsync();
            await _redisManager.AddAsync($"productAll", productList);
            return productList;
        }
        public override async Task<Product> GetByIdAsync(string id)
        {
            var cacheResult = await _redisManager.GetAsync<Product>($"product{id}");
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var product = await base.GetByIdAsync(id);
            await _redisManager.AddAsync($"product{id}", product);
            return product;
        }       
        public override async Task AddAsync(Product entity)
        {
            await _redisManager.RemoveAsync($"productAll");
            await base.AddAsync(entity);
        }
        public override async Task<Product> UpdateAsync(string id, Product entity)
        {
            await _redisManager.RemoveAsync($"product{entity.Id}");
            await _redisManager.RemoveAsync($"productAll");
            return await base.UpdateAsync(id, entity);
        }
        public override async Task<Product> DeleteAsync(string id)
        {
            await _redisManager.RemoveAsync($"product{id}");
            await _redisManager.RemoveAsync($"productAll");
            return await base.DeleteAsync(id);
        }

    }

}
