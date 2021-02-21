using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
using DevStore.Communication.Mediator;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Core.Messages.CommonMessages.Notifications;
using DevStore.Finance.AntiCorruption;
using DevStore.Finance.Business.Events;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Data;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

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
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<AlmostFullCourseEvent>, CourseEventHandler>();
            services.AddScoped<INotificationHandler<OrderStartedEvent>, CourseEventHandler>();
            services.AddScoped<INotificationHandler<OrderCanceledEvent>, CourseEventHandler>();

            // Sales
            services.AddScoped<SalesDomain.IOrderRepository, SalesData.Repository.OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinishOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderAndDisrollFromCourseCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemRemovedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderEmptyRemovedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderVoucherAppliedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderEnrolledRejectedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentRealizedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentRefusedEvent>, OrderEventHandler>();

            // Finance
            services.AddScoped<FinanceBusiness.IOrderRepository, FinanceData.Repository.OrderRepository>();
            services.AddScoped<FinanceBusiness.IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            services.AddScoped<FinanceBusiness.IPaymentService, FinanceBusiness.PaymentService>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<FinanceData.FinanceContext>();

            services.AddScoped<INotificationHandler<OrderEnrolledAcceptedEvent>, PaymentEventHandler>();
        }
    }
}