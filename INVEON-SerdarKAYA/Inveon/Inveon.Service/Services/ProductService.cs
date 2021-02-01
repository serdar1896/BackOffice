using Inveon.Core.Interfaces.CacheManagement;
using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Interfaces.Services;
using Inveon.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inveon.Service.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IRedisManagement _redismanager;

        public ProductService(IBaseRepository<Product> repository,IRedisManagement redismanager) : base(repository)
        {
            _redismanager = redismanager;
        }
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            var cacheResult = await _redismanager.GetAsync<List<Product>>($"productAll");
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var productList = await base.GetAllAsync();
            await _redismanager.AddAsync($"productAll", productList);
            return productList;
        }
        public override async Task<Product> GetByIdAsync(string id)
        {
            var cacheResult = await _redismanager.GetAsync<Product>($"product{id}");
            if (cacheResult != null)
            {
                return cacheResult;
            }
            var product = await base.GetByIdAsync(id);
            await _redismanager.AddAsync($"product{id}", product);
            return product;
        }       
        public override async Task AddAsync(Product entity)
        {
            await _redismanager.RemoveAsync($"productAll");
            await base.AddAsync(entity);
        }
        public override async Task<Product> UpdateAsync(string id, Product entity)
        {
            await _redismanager.RemoveAsync($"product{entity.Id}");
            await _redismanager.RemoveAsync($"productAll");
            return await base.UpdateAsync(id, entity);
        }
        public override async Task<Product> DeleteAsync(string id)
        {
            await _redismanager.RemoveAsync($"product{id}");
            await _redismanager.RemoveAsync($"productAll");
            return await base.DeleteAsync(id);
        }

    }

}
