using System;
using System.Reflection;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaLogData : ISagaLogData
    {
        public SagaId Id { get; set; }
        public string SagaType { get; set; }
        public long CreatedAt { get; set; }
        public object Message { get; set; }
        Type ISagaLogData.Type => Assembly.GetEntryAssembly()?.GetType(SagaType);
    }
}
