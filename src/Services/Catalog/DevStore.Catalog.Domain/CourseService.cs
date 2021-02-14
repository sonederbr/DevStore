using DevStore.Core.Bus;
using System;
using System.Threading.Tasks;
using DevStore.Catalog.Domain.Events;

namespace DevStore.Catalog.Domain
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatorHandler _bus;

        public CourseService(ICourseRepository courseRepository, 
                            IMediatorHandler bus)
        {
            _courseRepository = courseRepository;
            _bus = bus;
        }

        public async Task<bool> EnrolCourse(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null) return false;

            if (!course.HasVacancy()) return false;

            course.Enrol();

            // TODO: Parametrizar a quantidade de estoque baixo
            if ((course.TotalOfEnrolled - course.EnrollimentLimit) < 5)
            {
                await _bus.PublishEvent(new AlmostFullCourseEvent(course.Id, (course.TotalOfEnrolled - course.EnrollimentLimit)));
            }

            _courseRepository.Update(course);
            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DisenrollCourse(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null) return false;
            course.Disenrol();

            _courseRepository.Update(course);
            return await _courseRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _courseRepository?.Dispose();
        }
    }
}