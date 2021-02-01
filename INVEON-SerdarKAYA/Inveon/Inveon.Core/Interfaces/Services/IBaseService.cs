using Inveon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inveon.Core.Interfaces.Services
{
    public interface IBaseService<TModel> where TModel: class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>> GetByParamAsync(Expression<Func<TModel, bool>> predicate = null);
        Task<TModel> GetByIdAsync(string id);
        Task AddAsync(TModel entity);
        Task<bool> AddRangeAsync(IEnumerable<TModel> entities);
        Task<TModel> UpdateAsync(string id, TModel entity);
        Task<TModel> UpdateAsync(TModel entity, Expression<Func<TModel, bool>> predicate);
        Task<TModel> DeleteAsync(string id);
        Task<TModel> DeleteAsync(Expression<Func<TModel, bool>> predicate);
    }
}
