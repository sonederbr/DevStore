using System;

namespace DevStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public abstract class IntegrationEvent : Message
    {
        public DateTime Timestamp { get; private set; }

        protected IntegrationEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
