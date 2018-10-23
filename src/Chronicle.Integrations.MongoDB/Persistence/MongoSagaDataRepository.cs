using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    public class MongoSagaDataRepository : ISagaDataRepository
    {
        private const string CollectionName = "SagaData";
        private readonly IMongoCollection<MongoSagaData> _collection;

        public MongoSagaDataRepository(IMongoDatabase database)
            => _collection = database.GetCollection<MongoSagaData>(CollectionName);

        public async Task<ISagaData> ReadAsync(Guid sagaId, Type sagaType)
            => await _collection
                .Find(sld => sld.SagaId == sagaId && sld.SagaType == sagaType.FullName)
                .FirstOrDefaultAsync();

        public async Task WriteAsync(ISagaData sagaData)
        {
            await _collection.DeleteOneAsync(sld => sld.SagaId == sagaData.SagaId && sld.SagaType == sagaData.SagaType.FullName);
            await _collection.InsertOneAsync(new MongoSagaData
            {
                SagaId = sagaData.SagaId,
                SagaType = sagaData.SagaType.FullName,
                State = sagaData.State,
                Data = sagaData.Data
            });
        }
    }
}
