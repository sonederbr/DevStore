using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DevStore.Catalog.Domain.Events
{
    public class CourseEventHandler : INotificationHandler<AlmostFullCourseEvent>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseEventHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Handle(AlmostFullCourseEvent mensagem, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(mensagem.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }
    }
}