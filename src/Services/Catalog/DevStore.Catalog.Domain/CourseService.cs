using DevStore.Core.Communication.Bus;
using System;
using System.Threading.Tasks;
using DevStore.Catalog.Domain.Events;
using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Collections.Generic;

namespace DevStore.Catalog.Domain
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IBusHandler _mediatorHandler;

        public CourseService(ICourseRepository courseRepository, 
                            IBusHandler mediatorHandler)
        {
            _courseRepository = courseRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> EnrolCourse(Guid courseId)
        {
            if(!await DecreaseVacancyInCourse(courseId)) return false;

            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> EnrolCourse(IEnumerable<Guid> courses)
        {
            foreach (var courseId in courses)
            {
               await DecreaseVacancyInCourse(courseId);
            }

            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DisenrollCourse(Guid courseId)
        {
            if (!await IncreaseVacancyInCourse(courseId)) return false;

            return await _courseRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DisenrollCourse(IEnumerable<Guid> courses)
        {
            foreach (var courseId in courses)
            {
                await IncreaseVacancyInCourse(courseId);
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
            if ((course.EnrollimentLimit - course.TotalOfEnrolled) < 5)
            {
                await _mediatorHandler.PublishDomainEvent(new AlmostFullCourseEvent(course.Id, (course.TotalOfEnrolled - course.EnrollimentLimit)));
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