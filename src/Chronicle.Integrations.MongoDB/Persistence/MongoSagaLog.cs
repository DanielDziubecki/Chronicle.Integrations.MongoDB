using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal sealed class MongoSagaLog : ISagaLog
    {
        private const string CollectionName = "SagaLog";
        private readonly IMongoCollection<MongoSagaLogData> _collection;

        public MongoSagaLog(IMongoDatabase database)
            => _collection = database.GetCollection<MongoSagaLogData>(CollectionName);

        public async Task<IEnumerable<ISagaLogData>> ReadAsync(Guid sagaId, Type sagaType)
            => await _collection
                .Find(sld => sld.SagaId == sagaId && sld.SagaType == sagaType.FullName)
                .ToListAsync();

        public async Task WriteAsync(ISagaLogData message)
            => await _collection.InsertOneAsync(new MongoSagaLogData
            {
                SagaId = message.Id,
                SagaType = message.Type.FullName,
                Message = message.Message,
                CreatedAt = message.CreatedAt
            });
    }
}
