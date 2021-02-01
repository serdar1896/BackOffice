using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inveon.Service.Services
{
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        private readonly IBaseRepository<TModel> _repository;

        public BaseService(IBaseRepository<TModel> repository)
        {
            _repository = repository;
        }
        public virtual async Task AddAsync(TModel entity)
        {
           await _repository.AddAsync(entity);
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TModel> entities)
        {
            return await _repository.AddRangeAsync(entities);
        }

        public virtual async Task<TModel> DeleteAsync(string id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<TModel> DeleteAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _repository.DeleteAsync(predicate);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<TModel>> GetByParamAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            return await _repository.GetByParamAsync(predicate);
        }

        public virtual async Task<TModel> UpdateAsync(string id, TModel entity)
        {
            return await _repository.UpdateAsync(id,entity);
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity, Expression<Func<TModel, bool>> predicate)
        {
            return await _repository.UpdateAsync(entity,predicate);
        }
    }
}
