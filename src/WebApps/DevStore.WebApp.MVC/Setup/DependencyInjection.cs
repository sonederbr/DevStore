using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
using DevStore.Communication.Mediator;
using DevStore.Core.Data.EventSourcing;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Finance.AntiCorruption;
using DevStore.Finance.Business.Events;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Data;

using EventSourcing;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Rebus.Handlers;

using FinanceBusiness = DevStore.Finance.Business;
using FinanceData = DevStore.Finance.Data;
using SalesData = DevStore.Sales.Data;
using SalesDomain = DevStore.Sales.Domain;

namespace DevStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Bus
            services.AddScoped<IBusHandler, BusHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            // Catalog
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<CatalogContext>();

            // Domain Events
            services.AddScoped<IHandleMessages<AlmostFullCourseEvent>, CourseEventHandler>();
            
            // Integration Events
            services.AddScoped<IHandleMessages<OrderStartedEvent>, CourseEventHandler>();
            services.AddScoped<IHandleMessages<OrderCanceledEvent>, CourseEventHandler>();

            // Sales
            services.AddScoped<SalesDomain.IOrderRepository, SalesData.Repository.OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            //services.AddScoped<IHandleMessages<AddOrderItemCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<RemoveOrderItemCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<ApplyVoucherOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<StartOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<FinishOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<CancelOrderAndDisrollFromCourseCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<CancelOrderCommand>, OrderCommandHandler>();

            //services.AddScoped<IHandleMessages<OrderDraftStartedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderItemAddedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderUpdatedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderItemRemovedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderEmptyRemovedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderVoucherAppliedEvent>, OrderEventHandler>();

            // Finance
            services.AddScoped<FinanceBusiness.IOrderRepository, FinanceData.Repository.OrderRepository>();
            services.AddScoped<FinanceBusiness.IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            services.AddScoped<FinanceBusiness.IPaymentService, FinanceBusiness.PaymentService>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<FinanceData.FinanceContext>();

            services.AddScoped<IHandleMessages<OrderEnrolledAcceptedEvent>, PaymentEventHandler>();
        }
    }
}