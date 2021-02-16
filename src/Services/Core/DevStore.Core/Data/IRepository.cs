using System;
using DevStore.Core.Messages.CommonMessages.DomainEvents;

namespace DevStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}