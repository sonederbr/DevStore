﻿using System;

using MediatR;

namespace DevStore.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid DomainNotificationId { get; protected set; }
        public int Version { get; protected set; }
        public string Key { get; protected set; }
        public string Value { get; protected set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
