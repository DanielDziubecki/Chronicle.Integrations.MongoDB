using System;
using System.Reflection;

namespace Chronicle.Integrations.MongoDB.Persistence
{
    internal class MongoSagaState : ISagaState
    {
        public SagaId Id { get; set; }
        public string SagaType { get; set; }
        public SagaStates State { get; set; }
        public object Data { get; set; }
        Type ISagaState.Type => Assembly.GetEntryAssembly()?.GetType(SagaType);

        public void Update(SagaStates state, object data = null)
        {
            State = state;
            Data = data;
        }

 
    }
}
