using DevStore.Communication.Mediator;
using System;
using System.Threading.Tasks;
using DevStore.Catalog.Domain.Events;
using DevStore.Core.DomainObjects.DTO;

namespace DevStore.Catalog.Domain
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CourseService(ICourseRepository courseRepository, 
                            IMediatorHandler mediatorHandler)
        {
            _courseRepository = courseRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> EnrolCourse(Guid courseId)
        {
            if(!await DecreaseVacancyInCourse(courseId)) return false;

            return await _courseRepository.UnitOfWork.Commit();
        }
        public async Task<bool> EnrolCourse(CoursesOrderDto courses)
        {
            foreach (var item in courses.Items)
            {
               await DecreaseVacancyInCourse(item.CourseId);
            }

            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DisenrollCourse(Guid courseId)
        {
            if (!await IncreaseVacancyInCourse(courseId)) return false;
            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DisenrollCourse(CoursesOrderDto courses)
        {
            foreach (var item in courses.Items)
            {
                await IncreaseVacancyInCourse(item.CourseId);
            }

            return await _courseRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DecreaseVacancyInCourse(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null) return false;

            if (!course.HasVacancy()) return false;

            course.Enrol();

            // TODO: Parametrizar a quantidade de estoque baixo
            if ((course.TotalOfEnrolled - course.EnrollimentLimit) < 5)
            {
                await _mediatorHandler.PublishEvent(new AlmostFullCourseEvent(course.Id, (course.TotalOfEnrolled - course.EnrollimentLimit)));
            }

            _courseRepository.Update(course);

            return true;
        }

        private async Task<bool> IncreaseVacancyInCourse(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            if (course == null) return false;
            course.Disenrol();

            _courseRepository.Update(course);

            return true;
        }

        public void Dispose()
        {
            _courseRepository?.Dispose();
        }
    }
}