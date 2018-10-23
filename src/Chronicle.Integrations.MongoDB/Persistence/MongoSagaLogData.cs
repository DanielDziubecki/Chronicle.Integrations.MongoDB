using System;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaLogData : ISagaLogData
    {
        public Guid Id { get; set; }
        public Guid SagaId { get; set; }
        public string SagaType { get; set; }
        public long CreatedAt { get; set; }
        public object Message { get; set; }

        Type ISagaLogData.SagaType => Type.GetType(SagaType);
    }
}
