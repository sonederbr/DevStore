using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

using Rebus.Handlers;

namespace DevStore.Catalog.Domain.Events
{
    public class CourseEventHandler :
        IHandleMessages<AlmostFullCourseEvent>,
        IHandleMessages<OrderStartedEvent>,
        IHandleMessages<OrderCanceledEvent>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        private readonly IBusHandler _bus;

        public CourseEventHandler(
            ICourseRepository courseRepository,
            ICourseService courseService,
            IBusHandler bus)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
            _bus = bus;
        }

        public async Task Handle(AlmostFullCourseEvent message)
        {
            var course = await _courseRepository.GetById(message.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }

        public async Task Handle(OrderStartedEvent message)
        {
            var result = await _courseService.EnrolCourse(message.CourseIds);

            if (result)
            {
                await _bus.PublishIntegrationEvent(new OrderEnrolledAcceptedEvent(message.OrderId, 
                                                                                         message.ClientId, 
                                                                                         message.Total, 
                                                                                         message.NameCard, 
                                                                                         message.NumberCard, 
                                                                                         message.ExpirationDateCard, 
                                                                                         message.CvvCard,
                                                                                         message.CourseIds));
            }
            else
            {
               await  _bus.PublishIntegrationEvent(new OrderEnrolledRejectedEvent(message.OrderId, message.ClientId));
            }
        }

        public async Task Handle(OrderCanceledEvent message)
        {
            await _courseService.DisenrollCourse(message.CourseIds);
        }
    }
}