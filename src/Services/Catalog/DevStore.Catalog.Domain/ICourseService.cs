using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace DevStore.Catalog.Domain
{
    public interface ICourseService : IDisposable
    {
        Task<bool> EnrolCourse(Guid productId);
        Task<bool> DisenrollCourse(Guid productId);
        Task<bool> EnrolCourse(IEnumerable<Guid> courses);
        Task<bool> DisenrollCourse(IEnumerable<Guid> courses);
    }
}