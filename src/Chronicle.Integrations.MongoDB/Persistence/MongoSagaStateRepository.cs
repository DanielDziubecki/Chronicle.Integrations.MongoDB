using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaStateRepository : ISagaStateRepository
    {
        private const string CollectionName = "SagaData";
        private readonly IMongoCollection<MongoSagaState> _collection;

        public MongoSagaStateRepository(IMongoDatabase database)
            => _collection = database.GetCollection<MongoSagaState>(CollectionName);

        public async Task<ISagaState> ReadAsync(Guid sagaId, Type sagaType)
            => await _collection
                .Find(sld => sld.SagaId == sagaId && sld.SagaType == sagaType.FullName)
                .FirstOrDefaultAsync();

        public async Task WriteAsync(ISagaState sagaState)
        {
            await _collection.ReplaceOneAsync(sld => sld.SagaId == sagaState.Id && sld.SagaType == sagaState.Type.FullName,
                new MongoSagaState
                {
                    SagaId = sagaState.Id,
                    SagaType = sagaState.Type.FullName,
                    State = sagaState.State,
                    Data = sagaState.Data
                });
        }
    }
}
