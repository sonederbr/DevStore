using System.Threading;
using System.Threading.Tasks;

using DevStore.Communication.Mediator;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

using MediatR;

namespace DevStore.Catalog.Domain.Events
{
    public class CourseEventHandler :
        INotificationHandler<AlmostFullCourseEvent>,
        INotificationHandler<OrderStartedEvent>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        private readonly IMediatorHandler _mediatorHandler;

        public CourseEventHandler(
            ICourseRepository courseRepository,
            ICourseService courseService,
            IMediatorHandler mediatorHandler)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(AlmostFullCourseEvent message, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(message.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }

        public async Task Handle(OrderStartedEvent message, CancellationToken cancellationToken)
        {
            var result = await _courseService.EnrolCourse(message.CoursesOrder);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderEnrolledAcceptedEvent(message.OrderId, 
                                                                                         message.ClientId, 
                                                                                         message.Total, 
                                                                                         message.CoursesOrder, 
                                                                                         message.NameCard, 
                                                                                         message.NumberCard, 
                                                                                         message.ExpirationDateCard, 
                                                                                         message.CvvCard));
            }
            else
            {
               await  _mediatorHandler.PublishEvent(new OrderEnrolledRejectedEvent(message.OrderId, message.ClientId));
            }
        }
    }
}