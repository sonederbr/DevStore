using System;
using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Events;

using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace DevStore.Sales.Application
{
    public class OrderSaga : Saga<OrderSagaData>,
        IAmInitiatedBy<StartSagaCommand>,
        //IHandleMessages<OrderStartedEvent>,
        IHandleMessages<PaymentRealizedEvent>,
        IHandleMessages<OrderEnrolledRejectedEvent>,
        IHandleMessages<PaymentRefusedEvent>,
        IHandleMessages<OrderFinishedEvent>,
        IHandleMessages<OrderCanceledEvent>

    {
        private readonly IBusHandler _bus;

        public OrderSaga(IBusHandler bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderSagaData> config)
        {
            config.Correlate<StartSagaCommand>(m => m.AggregateId, d => d.Id);
            //config.Correlate<OrderStartedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<OrderEnrolledRejectedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<OrderEnrolledAcceptedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<PaymentRealizedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<PaymentRefusedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<OrderFinishedEvent>(m => m.AggregateId, d => d.Id);
            config.Correlate<OrderCanceledEvent>(m => m.AggregateId, d => d.Id);
        }

        /// <summary>
        //      StartOrderCommand :
        //      OrderStartedEvent > OrderEnrolledRejectedEvent > CancelOrderCommand
        //      OrderStartedEvent > OrderEnrolledAcceptedEvent > PaymentRealizedEvent > FinishOrderCommand > OrderFinishedEvent
        //      OrderStartedEvent > OrderEnrolledAcceptedEvent > PaymentRefusedEvent > CancelOrderAndDisrollFromCourseCommand > OrderCanceledEvent
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task Handle(StartSagaCommand message)
        {
            _bus.SendCommand(new StartOrderCommand(message.ClientId,
                                                        message.OrderId,
                                                        message.Total,
                                                        message.NameCard,
                                                        message.NumberCard,
                                                        message.ExpirationDateCard,
                                                        message.CvvCard)).Wait();

            Data.OrderStarted = true;

            ProcessSaga();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saga  StartSagaCommand");
            Console.ForegroundColor = ConsoleColor.Black;

            return Task.CompletedTask;
        }

        //public Task Handle(OrderStartedEvent message)
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine("Saga  OrderStartedEvent");
        //    Console.ForegroundColor = ConsoleColor.Black;

        //    return Task.CompletedTask;
        //}

        public Task Handle(PaymentRealizedEvent message)
        {
            _bus.SendCommand(new FinishOrderCommand(message.OrderId, message.ClientId)).Wait();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Saga  PaymentRealizedEvent");
            Console.ForegroundColor = ConsoleColor.Black;

            Data.PaymentAccepted = true;

            ProcessSaga();

            return Task.CompletedTask;
        }

        public Task Handle(OrderEnrolledRejectedEvent message)
        {
            _bus.SendCommand(new CancelOrderCommand(message.OrderId, message.ClientId)).Wait();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saga OrderEnrolledRejectedEvent!");
            Console.ForegroundColor = ConsoleColor.Black;

            Data.OrderCanceled = true;

            ProcessSaga();

            return Task.CompletedTask;
        }

        public Task Handle(PaymentRefusedEvent message)
        {
            _bus.SendCommand(new CancelOrderAndDisrollFromCourseCommand(message.OrderId, message.ClientId)).Wait();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Saga PaymentRefusedEvent");
            Console.ForegroundColor = ConsoleColor.Black;

            Data.OrderCanceled = true;

            ProcessSaga();

            return Task.CompletedTask;
        }

        public Task Handle(OrderFinishedEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saga OrderFinishedEvent");
            Console.ForegroundColor = ConsoleColor.Black;

            Data.OrderFinished = true;

            ProcessSaga();

            return Task.CompletedTask;
        }

        public Task Handle(OrderCanceledEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Saga OrderCanceledEvent");
            Console.ForegroundColor = ConsoleColor.Black;

            Data.OrderCanceled = true;

            ProcessSaga();

            return Task.CompletedTask;
        }

        public void ProcessSaga()
        {
            if (Data.SagaIsCompleted)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Finalizando Saga!");
                Console.ForegroundColor = ConsoleColor.Black;

                MarkAsComplete();
            }
        }
    }
}