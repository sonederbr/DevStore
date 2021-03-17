using System;

namespace DevStore.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime DateAt, string data)
        {
            Id = id;
            Type = type;
            this.DateAt = DateAt;
            Data = data;
        }

        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public DateTime DateAt { get; set; }

        public string Data { get; private set; }
    }
}