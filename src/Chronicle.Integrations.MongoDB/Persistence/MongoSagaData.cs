using System;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public Guid SagaId { get; set; }
        public string SagaType { get; set; }
        public SagaStates State { get; set; }
        public object Data { get; set; }

        Type ISagaData.SagaType => Type.GetType(SagaType);
    }
}
