using System;
using System.Threading.Tasks;

using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Catalog.Domain
{
    public interface ICourseService : IDisposable
    {
        Task<bool> EnrolCourse(Guid productId);
        Task<bool> DisenrollCourse(Guid productId);
        Task<bool> EnrolCourse(CoursesOrderDto courses);
        Task<bool> DisenrollCourse(CoursesOrderDto courses);
    }
}