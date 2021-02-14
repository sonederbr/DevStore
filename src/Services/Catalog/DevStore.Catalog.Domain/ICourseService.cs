using System;
using System.Threading.Tasks;

namespace DevStore.Catalog.Domain
{
    public interface ICourseService : IDisposable
    {
        Task<bool> EnrolCourse(Guid productId);
        Task<bool> DisenrollCourse(Guid productId);
    }
}