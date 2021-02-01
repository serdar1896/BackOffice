using Inveon.Core.Interfaces.DbConfigs;
using Inveon.Core.Interfaces.Repositories;
using Inveon.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inveon.Data.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseEntity
    {
        private readonly IMongoCollection<TModel> mongoCollection;

        public BaseRepository(IMongoConfiguration settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mongoCollection = database.GetCollection<TModel>(typeof(TModel).Name.ToLowerInvariant());
        } 

        public async Task<IEnumerable<TModel>> GetByParamAsync(Expression<Func<TModel, bool>> predicate = null)
        {
            return predicate == null
                ? await mongoCollection.AsQueryable().ToListAsync()
                : mongoCollection.AsQueryable().Where(predicate).ToList();
        }
     
        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await mongoCollection.Find(x => true).ToListAsync();
        }      


        public async Task<TModel> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TModel entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
             await mongoCollection.InsertOneAsync(entity, options);
        }

        public async Task<bool> AddRangeAsync(IEnumerable<TModel> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await mongoCollection.BulkWriteAsync((IEnumerable<WriteModel<TModel>>)entities, options)).IsAcknowledged;
        }

        public async Task<TModel> UpdateAsync(string id, TModel entity)
        {
                return await mongoCollection.FindOneAndReplaceAsync(x => x.Id == id, entity);

        }

        public async Task<TModel> UpdateAsync(TModel entity, Expression<Func<TModel, bool>> predicate)
        {
            return await mongoCollection.FindOneAndReplaceAsync(predicate, entity);
        }

        public async Task<TModel> DeleteAsync(string id)
        {
            return await mongoCollection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public async Task<TModel> DeleteAsync(Expression<Func<TModel, bool>> filter)
        {
            return await mongoCollection.FindOneAndDeleteAsync(filter);
        }

    }  
}
