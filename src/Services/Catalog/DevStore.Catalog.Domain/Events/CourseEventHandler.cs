using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DevStore.Catalog.Domain.Events
{
    public class CourseEventHandler : INotificationHandler<LowStockCourseEvent>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseEventHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Handle(LowStockCourseEvent mensagem, CancellationToken cancellationToken)
        {
            var produto = await _courseRepository.GetById(mensagem.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }
    }
}