using System;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaState : ISagaState
    {
        public Guid Id { get; set; }
        public Guid SagaId { get; set; }
        public string SagaType { get; set; }
        public SagaStates State { get; set; }
        public object Data { get; set; }

        Type ISagaState.Type => Type.GetType(SagaType);
        
        public void Update(SagaStates state, object data = null)
        {
            State = state;
            Data = data;
        }
    }
}
