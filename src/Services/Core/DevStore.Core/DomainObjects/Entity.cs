using System;
using System.Collections.Generic;

using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace DevStore.Core.Messages.CommonMessages.DomainEvents
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        private List<Event> _notifications;
        private List<IntegrationEvent> _integratedNotifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();
        public IReadOnlyCollection<IntegrationEvent> IntegratedNotifications => _integratedNotifications?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AddEvent(Event @event)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(@event);
        }

        public void RemoveEvent(Event @event)
        {
            _notifications?.Remove(@event);
        }

        public void AddIntegrationEvent(IntegrationEvent @event)
        {
            _integratedNotifications = _integratedNotifications ?? new List<IntegrationEvent>();
            _integratedNotifications.Add(@event);
        }

        public void RemoveIntegrationEvent(IntegrationEvent @event)
        {
            _integratedNotifications?.Remove(@event);
        }

        public void ClearIntegrationEvents()
        {
            _integratedNotifications?.Clear();
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}